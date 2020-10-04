using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interruptor : MonoBehaviour
{
    public SpriteRenderer inter, border;
    public Color litColor;
    Color baseColor;
    Vector3 startPos;
    public Vector3 onPos;
    public UnityEvent onResponse, offResponse;

    public List<SpriteRenderer> dots = new List<SpriteRenderer>();

    private void Awake()
    {
        startPos = inter.transform.localPosition;
        baseColor = border.color;
    }

    public void On()
    {
        foreach(SpriteRenderer sr in dots)
        {
            sr.color = litColor;
        }
        border.color = litColor;
        inter.transform.localPosition = onPos;
        onResponse.Invoke();
    }

    public void Off()
    {
        foreach (SpriteRenderer sr in dots)
        {
            sr.color = baseColor;
        }
        border.color = baseColor;
        inter.transform.localPosition = startPos;
        offResponse.Invoke();
    }

    public void ResetInterruptor()
    {
        foreach (SpriteRenderer sr in dots)
        {
            sr.color = baseColor;
        }
        border.color = baseColor;
        inter.transform.localPosition = startPos;
    }

}
