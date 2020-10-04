using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Easings;

public class Door : MonoBehaviour
{
    public bool open;
    public Point pointA, pointB;

    public SpriteRenderer doorA, doorB;
    public float scaleYOpen, scaleYClose;

    public AutomaticSlider slider;

    private void Awake()
    {
        if (pointA != null)
        {
            pointA.doors.Add(this);
        }
        if (pointB != null)
        {
            pointB.doors.Add(this);
        }
    }

    public void AnimateDoor(float value)
    {
        Vector3 startScale = new Vector3(doorA.transform.localScale.x, scaleYClose, doorA.transform.localScale.z);
        Vector3 targetScale = new Vector3(doorA.transform.localScale.x, scaleYOpen, doorA.transform.localScale.z);
        doorA.transform.localScale = Vector3.Lerp(startScale, targetScale, value);
        doorB.transform.localScale = Vector3.Lerp(startScale, targetScale, value);
    }

    public void Open()
    {
        open = true;
        slider.enabled = true;
        slider.Reversed = false;
    }

    public void Close()
    {
        open = false;
        slider.enabled = true;
        slider.Reversed = true;
    }

    public void ResetDoor(bool o)
    {
        open = o;
        if (open)
        {
            slider.value = 1;
            slider.enabled = true;
            slider.Reversed = false;
            doorA.transform.localScale = new Vector3(doorA.transform.localScale.x, scaleYOpen, doorA.transform.localScale.z);
            doorB.transform.localScale = new Vector3(doorA.transform.localScale.x, scaleYOpen, doorA.transform.localScale.z);
        }
        else
        {
            slider.value = 0;
            slider.enabled = false;
            slider.Reversed = true;
            doorA.transform.localScale = new Vector3(doorA.transform.localScale.x, scaleYClose, doorA.transform.localScale.z);
            doorB.transform.localScale = new Vector3(doorA.transform.localScale.x, scaleYClose, doorA.transform.localScale.z);
        }
    }
}
