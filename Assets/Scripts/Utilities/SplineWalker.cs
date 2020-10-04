using UnityEngine;
using Easings;

public class SplineWalker : MonoBehaviour {

    bool move;

	public BezierSpline spline;

	public float duration;

	public bool lookForward;

	public SplineWalkerMode mode;

    public enum EaseType { SmoothStep, SmootherStep, EaseOut, EaseIn, ExpOut};

    public EaseType easing;

	private float progress;
	private bool goingForward = true;

	private void Update () {
        if (!move)
        {
            return;
        }

		if (goingForward) {
			progress += Time.deltaTime / duration;
			if (progress > 1f) {
				if (mode == SplineWalkerMode.Once) {
					progress = 1f;
				}
				else if (mode == SplineWalkerMode.Loop) {
					progress -= 1f;
				}
				else {
					progress = 2f - progress;
					goingForward = false;
				}
			}
		}
		else {
			progress -= Time.deltaTime / duration;
			if (progress < 0f) {
				progress = -progress;
				goingForward = true;
			}
		}

        
        float easedProgress = progress;

        switch (easing)
        {
            case EaseType.SmoothStep:
                easedProgress = Ease.SmoothStep(progress);
                break;
            case EaseType.SmootherStep:
                easedProgress = Ease.SmootherStep(progress);
                break;
            case EaseType.EaseIn:
                easedProgress = Ease.EaseIn(progress);
                break;
            case EaseType.EaseOut:
                easedProgress = Ease.EaseOut(progress);
                break;
            case EaseType.ExpOut:
                easedProgress = Ease.ExpOut(progress);
                break;
        }

		Vector3 position = spline.GetPoint(progress);
		transform.position = position;
		if (lookForward) {
			transform.LookAt(position + spline.GetDirection(progress));
		}
	}

    public void StartMoving()
    {
        move = true;
    }
}