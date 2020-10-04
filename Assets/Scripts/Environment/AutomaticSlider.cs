using UnityEngine;
using UnityEngine.Events;

public class AutomaticSlider : MonoBehaviour {

	[SerializeField, Min(0.01f)]
	float duration = 1f;

    [SerializeField, Min(0f)]
    float delay = 0f;


    [SerializeField]
	bool autoReverse = false, smoothstep = false;

	[System.Serializable]
	public class OnValueChangedEvent : UnityEvent<float> { }

	[SerializeField]
	OnValueChangedEvent onValueChanged = default;

    [SerializeField]
    UnityEvent endEvent = default;
    [SerializeField]
    UnityEvent endReverseEvent = default;

    [HideInInspector]
    public float value;
    public float delayValue;

    public bool Reversed { get; set; }

	public bool AutoReverse {
		get => autoReverse;
		set => autoReverse = value;
	}

	float SmoothedValue => 3f * value * value - 2f * value * value * value;

	void FixedUpdate () {
        float delta;

        if (Reversed) {
            if (delayValue > 0 )
            {
                delta = Time.deltaTime;
                delayValue -= delta;
            }
            else
            {
                delta = Time.deltaTime / duration;
                value -= delta;
            }
            if (value <= 0f) {
                endReverseEvent.Invoke();
				if (autoReverse) {
					value = Mathf.Min(1f, -value);
					Reversed = false;
				}
				else {
					value = 0f;
					enabled = false;
				}
			}
		}
		else {
            if (delayValue < delay )
            {
                delta = Time.deltaTime;
                delayValue += delta;
            }
            else
            {
                delta = Time.deltaTime / duration;
                value += delta;
            }
			if (value >= 1f) {
				if (autoReverse) {
					value = Mathf.Max(0f, 2f - value);
					Reversed = true;
				}
				else {
					value = 1f;
					enabled = false;
				}
                endEvent.Invoke();
			}
		}
		onValueChanged.Invoke(smoothstep ? SmoothedValue : value);
	}
}
