using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easings;
public class Fragment : MonoBehaviour
{
    public int fragmentID;
    public MeshRenderer fragmentPickup;
    public GameObject fragment;
    public GameObject shadow;
    public Material pickUpHiddenMat;
    public Material pickUpMat;
    public bool pickedUp;

    Kino.AnalogGlitch glitchAnalog;
    Kino.DigitalGlitch glitchDigital;

    public float scanlineJitter, verticalJump, horizontalShake, colorDrift;
    public float digitalIntensity;

    public float glitchTime;
    public SoundPlayer sp;

    void Awake()
    {
        glitchAnalog = Camera.main.GetComponent<Kino.AnalogGlitch>();
        glitchDigital = Camera.main.GetComponent<Kino.DigitalGlitch>();
    }

    public void PickUp()
    {
        if (!pickedUp)
        {
            sp.PlaySound(0);
            pickedUp = true;
            fragmentPickup.material = pickUpMat;
            fragment.SetActive(false);
            shadow.SetActive(false);
            StartCoroutine(Glitching());
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

    public void StopGlitch()
    {
        StopAllCoroutines();
    }

    public void ResetFragment()
    {
        pickedUp = false;
        fragment.SetActive(true);
        shadow.SetActive(true);
        fragmentPickup.material = pickUpHiddenMat;
    }
}
