using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationInterpolator : MonoBehaviour
{
    [SerializeField]
    Rigidbody body = default;

    [SerializeField]
    Vector3 from = default, to = default;



    public void Interpolate(float t)
    {
        Quaternion r;

        r = Quaternion.LerpUnclamped(Quaternion.Euler(from), Quaternion.Euler(to), t);

        body.MoveRotation(r);
    }
}
