using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyEvent : MonoBehaviour
{
    public KeyCode code;
    public UnityEngine.Events.UnityEvent response;

    void Update()
    {
        if (Input.GetKeyDown(code))
        {
            response.Invoke();
        }
        
    }
}
