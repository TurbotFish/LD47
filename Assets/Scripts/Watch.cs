using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easings;

public class Watch : MonoBehaviour
{
    public LevelInfo levelInfo;
    public Animator anim;
    public Clock clock;
    public Traveller traveller;
    public List<Fragment> fragments;

    Kino.AnalogGlitch glitchAnalog;
    Kino.DigitalGlitch glitchDigital;

    public float scanlineJitter, verticalJump, horizontalShake, colorDrift;
    public float digitalIntensity;

    public float glitchTime;

    public EndLevel endLevel;
    bool pickedup;
    public SoundPlayer sp;
    public ParticleSystem ps;

    void Awake()
    {
        glitchAnalog = Camera.main.GetComponent<Kino.AnalogGlitch>();
        glitchDigital = Camera.main.GetComponent<Kino.DigitalGlitch>();
    }

    public void PickUp()
    {
        if(pickedup)
        {
            return;
        }
        pickedup = true;
        StartCoroutine(Glitching());
        clock.Pause();
        anim.SetBool("pickup", true);
        traveller.LockMovement(true);
        endLevel.Blur();
        levelInfo.success = true;
        clock.RegisterTime();
        for(int i = 0;i<fragments.Count;i++)
        {
            if (fragments[i].fragmentID < levelInfo.fragments.Length && fragments[i].pickedUp)
            {
                levelInfo.fragments[fragments[i].fragmentID] = true;
            }
        }
    }

    IEnumerator Glitching()
    {
        glitchAnalog.enabled = true;
        glitchDigital.enabled = true;
        float t = 0;
        while (t < glitchTime)
        {
            t += Time.deltaTime;
            t = Mathf.Clamp(t, 0f, glitchTime);

            float l = Mathf.Sin(Mathf.PI * (t / glitchTime));
            glitchAnalog.scanLineJitter = Mathf.Lerp(0, scanlineJitter, Ease.SmoothStep(l));
            glitchAnalog.verticalJump = Mathf.Lerp(0, verticalJump, Ease.SmoothStep(l));
            glitchAnalog.horizontalShake = Mathf.Lerp(0, horizontalShake, Ease.SmoothStep(l));
            glitchAnalog.colorDrift = Mathf.Lerp(0, colorDrift, Ease.SmoothStep(l));
            glitchDigital.intensity = Mathf.Lerp(0, digitalIntensity, Ease.SmoothStep(l));
            yield return null;
        }

        glitchAnalog.enabled = false;
        glitchDigital.enabled = false;
    }

    public void Explode()
    {
        ps.Play();
    }
}
