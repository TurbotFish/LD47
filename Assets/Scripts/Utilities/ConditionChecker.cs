using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent response;
    public List<bool> conditions = new List<bool>();
    public float delay = 0;

    public void SetConditonTrue(int id)
    {
        if (id>=0 && id<conditions.Count)
        {
            conditions[id] = true;

            bool test = true;
            foreach(bool b in conditions)
            {
                if (!b)
                {
                    test = false;
                }
            }
            if (test)
            {
                StartCoroutine(DelayedResponse());
            }
        }
    }

    public void SetConditonFalse(int id)
    {
        if (id >= 0 && id < conditions.Count)
        {
            conditions[id] = false;
        }
    }

    IEnumerator DelayedResponse()
    {
        yield return new WaitForSeconds(delay);
        response.Invoke();
    }
}
