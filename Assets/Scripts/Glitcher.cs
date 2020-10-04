using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easings;
public class Glitcher : MonoBehaviour
{
    Kino.AnalogGlitch glitchAnalog;
    Kino.DigitalGlitch glitchDigital;

    public float scanlineJitter, verticalJump, horizontalShake, colorDrift;
    public float digitalIntensity;

    public float glitchTime;

    void Awake()
    {
        glitchAnalog = Camera.main.GetComponent<Kino.AnalogGlitch>();
        glitchDigital = Camera.main.GetComponent<Kino.DigitalGlitch>();
    }
    public void GlitchIn()
    {
        StartCoroutine(Glitching(true));
    }
    public void GlitchOut()
    {
        StartCoroutine(Glitching(false));
    }
    IEnumerator Glitching(bool forward)
    {
        glitchAnalog.enabled = true;
        glitchDigital.enabled = true;
        float t = (forward)? 0:glitchTime;
        bool glitch = true;
        while (glitch)
        {
            t =(forward)?t+ Time.deltaTime:t-Time.deltaTime;
            t = Mathf.Clamp(t, 0f, glitchTime);
            if (forward)
            {
                if (t>= glitchTime)
                {
                    glitch = false;
                }
            }
            else
            {
                if (t<=0)
                {
                    glitch = false;
                }
            }
            float l = (t / glitchTime);
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

    public void StopGlitch()
    {
        StopAllCoroutines();
    }
}
