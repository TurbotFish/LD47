using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easings;
using UnityEngine.Events;
public class Point : MonoBehaviour
{
    public Transform point_parent;
    public Transform inSprite, outSprite;
    public float transformTime = 0.5f;
    public Vector3 inScaleFree, outScaleFree;
    public Vector3 inScaleOcc, outScaleOcc;

    public Point right, left, up, down;

    [HideInInspector]
    public List<Door> doors = new List<Door>();

    public UnityEvent enterResponse;
    public UnityEvent exitResponse;
    [HideInInspector]
    public int nbOnTop;
    public SoundPlayer sp;

    public void Switch(bool o)
    {
        StartCoroutine(Switching(o));
    }

    IEnumerator Switching(bool occupied)
    {
        if(occupied)
        {
            nbOnTop++;
            sp.PlaySound(0);
        }
        else
        {
            nbOnTop--;
        }
        Vector3 startScaleIn = !occupied ? inScaleOcc :inScaleFree;
        Vector3 startScaleOut = !occupied ? outScaleOcc : outScaleFree;
        Vector3 startEuler = occupied ? new Vector3(0, 0, 45): Vector3.zero;

        Vector3 targetScaleIn = !occupied ? inScaleFree : inScaleOcc;
        Vector3 targetScaleOut = !occupied ? outScaleFree : outScaleOcc;
        Vector3 targetEuler = occupied ? Vector3.zero : new Vector3(0, 0, 45);
        float t = 0;
        while (t<transformTime)
        {
            t += Time.deltaTime;
            float l = Mathf.Clamp(Ease.SmoothStep(t / transformTime), 0, 1);
            inSprite.localScale = Vector3.Lerp(startScaleIn, targetScaleIn, l);
            outSprite.localScale = Vector3.Lerp(startScaleOut, targetScaleOut, l);
            point_parent.localRotation = Quaternion.Euler(Vector3.Lerp(startEuler, targetEuler, l));
            yield return null;
        }
    }

    public void ResetPoint()
    {
        nbOnTop = 0;
    }
}
