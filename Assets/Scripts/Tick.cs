using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tick : MonoBehaviour
{
    public SpriteRenderer border;
    public UnityEvent tickResponse;
    [HideInInspector]
    public Color tickedColor;
    Color baseColor;
    private void Awake()
    {
        baseColor = border.color;
    }
    public void Trigger()
    {
        tickResponse.Invoke();
        border.color = tickedColor;
    }

    public void Reset()
    {
        border.color = baseColor;
    }
}
