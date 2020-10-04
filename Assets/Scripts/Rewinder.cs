using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easings;
public class Rewinder : MonoBehaviour
{
    Kino.AnalogGlitch glitchAnalog;
    Kino.DigitalGlitch glitchDigital;

    public float scanlineJitter, verticalJump, horizontalShake, colorDrift;
    public float digitalIntensity;

    public float rewindTime;
    public GameEvent startRewindEvent, midRewindEvent, endRewingEvent;
    public SoundPlayer soundPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        glitchAnalog = Camera.main.GetComponent<Kino.AnalogGlitch>();
        glitchDigital = Camera.main.GetComponent<Kino.DigitalGlitch>();
    }

    public void Rewind()
    {
        StartCoroutine(Rewinding());
    }

    IEnumerator Rewinding()
    {
        soundPlayer.PlaySound(0);
        startRewindEvent.Raise();
        glitchAnalog.enabled = true;
        glitchDigital.enabled = true;
        float t = 0;
        bool mid = false;
        while (t<rewindTime)
        {
            t += Time.deltaTime;
            t = Mathf.Clamp(t, 0f, rewindTime);
            if (!mid && t>rewindTime/2)
            {
                mid = true;
                midRewindEvent.Raise();
            }
            float l = Mathf.Sin(Mathf.PI * (t / rewindTime));
            glitchAnalog.scanLineJitter = Mathf.Lerp(0, scanlineJitter, Ease.SmoothStep(l));
            glitchAnalog.verticalJump = Mathf.Lerp(0, verticalJump, Ease.SmoothStep(l));
            glitchAnalog.horizontalShake = Mathf.Lerp(0, horizontalShake, Ease.SmoothStep(l));
            glitchAnalog.colorDrift = Mathf.Lerp(0, colorDrift, Ease.SmoothStep(l));
            glitchDigital.intensity = Mathf.Lerp(0, digitalIntensity, Ease.SmoothStep(l));
            yield return null;
        }
        glitchAnalog.enabled = false;
        glitchDigital.enabled = false;
        endRewingEvent.Raise();
    }
}
