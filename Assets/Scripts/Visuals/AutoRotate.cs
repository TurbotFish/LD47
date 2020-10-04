using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public Vector3 rot;
    void Update()
    {
        transform.Rotate(rot*Time.deltaTime);
    }
}
