using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easings;

public class Shaker : MonoBehaviour
{
    public Camera cam;
    bool shakingInfinite;
    public float hardShakeMagnitude;
    public float softShakeMagnitude;
    Vector3 basePos;
    private void Awake()
    {
        basePos = cam.transform.localPosition;
    }
    public void ShakeSoft(float duration)
    {
        StartCoroutine(Shaking(duration, softShakeMagnitude));
    }

    public void ShakeHard(float duration)
    {
        StartCoroutine(Shaking(duration, hardShakeMagnitude));
    }

    public void StartShake(float magnitude)
    {
        StartCoroutine(ShakingInfinite(magnitude));
    }

    public void StopShaking()
    {
        shakingInfinite = false;
    }

    IEnumerator Shaking(float duration, float magnitude)
    {
        float elapsed = 0.0f;
        while (elapsed<duration)
        {
            float t = Ease.EaseOut(elapsed / duration);
            float mag = Mathf.Lerp(magnitude, 0, t);
            float x = Random.Range(-1f, 1f) * mag;
            float y = Random.Range(-1f, 1f) * mag;

            cam.transform.localPosition = basePos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = basePos;
    }

    IEnumerator ShakingInfinite(float magnitude)
    {
        shakingInfinite = true;
        while (shakingInfinite)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cam.transform.localPosition = basePos + new Vector3(x, y, 0);
            yield return null;
        }
        cam.transform.localPosition = basePos;
    }


}
