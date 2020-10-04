using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeEvent : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent response;

    private void Awake()
    {
        response.Invoke();    
    }
}
