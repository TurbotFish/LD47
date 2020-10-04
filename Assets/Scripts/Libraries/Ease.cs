using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Easings
{
    class Ease
    {
        public static float SmoothStep(float t)
        {
            float _t = t;
            _t = _t * _t * (3f - 2f * _t);            
            return _t;
        }

        public static float SmootherStep(float t)
        {
            float _t = t;
            _t = _t * _t * _t * (_t * (6f * _t - 15f) + 10f);
            return _t;
        }

        public static float ExpOut(float t)
        {
            float _t = t;
            _t = _t * _t;
            return _t;
        }

        public static float EaseOut(float t)
        {
            float _t = t;
            _t = Mathf.Sin(_t * Mathf.PI * 0.5f);
            return _t;
        }

        public static float EaseIn(float t)
        {
            float _t = t;
            _t = 1f - Mathf.Cos(_t * Mathf.PI * 0.5f);
            return _t;
        }
    }

}
