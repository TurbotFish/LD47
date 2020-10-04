using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boing : MonoBehaviour
{
    public AnimationCurve scaleYCurve;
    [HideInInspector]
    public float t;
    public float time;
    float startScaleY;

    private void Awake()
    {
        startScaleY = transform.localScale.y;
    }

    void Update()
    {
        if (t<time)
        {
            t += Time.deltaTime;
            float y = startScaleY + scaleYCurve.Evaluate(t / time);
            Vector3 scale = new Vector3(transform.localScale.x, y, transform.localScale.z);
            transform.localScale = scale;
        }
    }
}
