using UnityEngine;

public class PositionInterpolator : MonoBehaviour {

	[SerializeField]
	Vector3 from = default, to = default;



    public void Interpolate (float t) {
		Vector3 p;
        p = Vector3.LerpUnclamped(from, to, t);
        transform.localPosition = p;
	}
}