using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Easings;

public class Menu : MonoBehaviour
{
    PostProcessVolume ppv;
    DepthOfField dof;
    public float maxFocalLength;
    public float blurTime;
    public GameEvent endBlurEvent;
    IEnumerator blurRoutine;
    public UnityEngine.Events.UnityEvent yesResponse, noResponse;
    bool activated;
    
    private void Start()
    {
        ppv = Camera.main.GetComponent<PostProcessVolume>();
    }

    private void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A))
            {
                yesResponse.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Escape))
            {
                noResponse.Invoke();
            }
        }
    }

    public void RestartLevel()
    {

    }

    public void Blur(bool b)
    {
        if(blurRoutine!=null)
        {
            StopCoroutine(blurRoutine);
        }
        blurRoutine = Blurring(0, b);
        StartCoroutine(blurRoutine);
    }

    public void Activate(bool b)
    {
        activated = b;
    }

    public void UnpauseClock()
    {
        FindObjectOfType<Clock>().Unpause();
    }

    public void PauseClock()
    {
        FindObjectOfType<Clock>().Pause();
    }

    public void LockPlayer(bool b)
    {
        FindObjectOfType<Traveller>().LockMovement(b);
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
            float l = Mathf.Clamp(Ease.SmoothStep(t / blurTime),0,1);
            dof.focalLength.value = (b) ? Mathf.Lerp(1, maxFocalLength, l) : Mathf.Lerp(maxFocalLength, 1, l);
            yield return null;
        }
        if (!b)
        {
            endBlurEvent.Raise();
        }
    }
}
