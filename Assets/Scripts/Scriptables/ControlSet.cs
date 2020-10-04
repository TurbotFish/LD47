using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Other/Control Set")]
public class ControlSet : ScriptableObject
{
    public bool disabled;
    public GameEvent fireWhenDisabled;
    public GameEvent fireWhenEnabled;
    bool wasPreviouslyDisabled;

    public void Disable()
    {

        disabled = true;
        if (fireWhenDisabled != null)
        {
            fireWhenDisabled.Raise();

        }
    }

    public void Enable()
    {
        disabled = false;
        if (fireWhenEnabled != null)
        {
            fireWhenEnabled.Raise();

        }
    }

    public void DisableWithSave()
    {
        if (disabled)
        {
            wasPreviouslyDisabled = true;
        }
        else
        {
            wasPreviouslyDisabled = false;
            Disable();
        }
    }

    public void EnableWithSave()
    {
        if (!wasPreviouslyDisabled)
        {
            Enable();
        }
    }
}
