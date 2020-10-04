using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MouseReaction : MonoBehaviour
{
    public bool disabled;
    public ControlSet controls;
    bool leftClicked;

    public int currentSet;

    public List<MouseReactionSet> reactionSets = new List<MouseReactionSet>();

    private void OnMouseEnter()
    {
        if (!controls.disabled && !disabled)
        {
            reactionSets[currentSet].mouseEnterResponse.Invoke();
        }
    }

    private void OnMouseExit()
    {
        if (!controls.disabled && !disabled)
        {
            reactionSets[currentSet].mouseExitResponse.Invoke();
        }
    }

    private void OnMouseOver()
    {
        if (!controls.disabled && !disabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                reactionSets[currentSet].LeftClickResponse.Invoke();
                leftClicked = true;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (leftClicked)
                {
                    reactionSets[currentSet].LeftClickReleaseResponse.Invoke();
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //reactionSets[currentSet].RightClickResponse.Invoke();
            }
        }
       
    }

    public void SetReactionSet(int i)
    {
        currentSet = i;
    }

    public void DisableReaction()
    {
        disabled = true;
    }
    public void EnableReaction()
    {
        disabled = false;
    }
}

[Serializable]
public class MouseReactionSet
{
    public string title;
    public UnityEvent mouseEnterResponse;
    public UnityEvent mouseExitResponse;
    public UnityEvent LeftClickResponse;
    public UnityEvent LeftClickReleaseResponse;
    //public UnityEvent RightClickResponse;
}


