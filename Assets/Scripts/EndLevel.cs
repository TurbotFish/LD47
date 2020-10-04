using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easings;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class EndLevel : MonoBehaviour
{
    public LevelInfo levelInfo;

    public Animator anim;

    public PostProcessVolume ppv;
    public float startBlurDuration, endBlurDelay;
    DepthOfField dof;
    public float maxFocalLength;
    public float blurTime;
    public GameEvent endLevelEvent;
    public GameEvent startLevelEvent;
    public Material fragmentMat;
    public List<MeshRenderer> fragments;
    public TextMeshPro tmp_timer;
    public Clock clock;
    public Traveller traveller;
    public PressKeyEvent pke;
    SoundPlayer sp;

    private void Awake()
    {
        traveller.LockMovement(true);
        clock.Pause();
        ppv.profile.TryGetSettings<DepthOfField>(out dof);
        dof.enabled.value = true;
        dof.focalLength.value = maxFocalLength;
        StartCoroutine(Blurring(startBlurDuration, false));
        anim.SetTrigger("start");
        sp = GetComponent<SoundPlayer>();
    }

    public void Blur()
    {
        StartCoroutine(Blurring(endBlurDelay, true));
    }

    public void HighlightFragments()
    {
        StartCoroutine(Highlighting());
    }

    IEnumerator Blurring(float d, bool b)
    {
        yield return new WaitForSeconds(d);

        ppv.profile.TryGetSettings<DepthOfField>(out dof);
        dof.enabled.value = true;
        float t = 0;
        while (t < blurTime)
        {
            t += Time.deltaTime;
            t = Mathf.Clamp(t, 0, blurTime);
            dof.focalLength.value = (b)? Mathf.Lerp(1, maxFocalLength, Ease.SmoothStep(t / blurTime)): Mathf.Lerp(maxFocalLength, 1, Ease.SmoothStep(t / blurTime));
            yield return null;
        }
        if(b)
        {
            endLevelEvent.Raise();
            anim.SetTrigger("end");
            tmp_timer.text = levelInfo.ConvertTimeToTimer(levelInfo.time);
        }
        else
        {
            startLevelEvent.Raise();
        }
    }

    IEnumerator Highlighting()
    {
        for(int i = 0;i<levelInfo.fragments.Length;i++)
        {
            if (levelInfo.fragments[i] == true)
            {
                if (i<fragments.Count)
                {
                    fragments[i].material = fragmentMat;
                    sp.PlaySound(0);
                    yield return new WaitForSeconds(0.25f);
                }
            }
        }
    }

    public void EnablePKE()
    {
        pke.enabled = true;
    }
    public void DisablePKE()
    {
        pke.enabled = false;
    }
}
