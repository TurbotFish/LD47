using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventFunctions : MonoBehaviour
{
    public Animator anim;


    public void SetAnimBoolToTrue(string boolName)
    {
        anim.SetBool(boolName, true);
    }
    public void SetAnimBoolToFalse(string boolName)
    {
        anim.SetBool(boolName, false);
    }
}
