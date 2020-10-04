using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalFunction : MonoBehaviour
{
    public int responseID = 0;
    public List<UnityEngine.Events.UnityEvent> responses = new List<UnityEngine.Events.UnityEvent>();


    public void SetResponseTo(int id)
    {
        if (id>=0 && id<responses.Count)
        {
            responseID = id;
        }
        else
        {
            Debug.Log("wrong id");
        }
    }

    public void Trigger()
    {
        responses[responseID].Invoke();
    }

}
