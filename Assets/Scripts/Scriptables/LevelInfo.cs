using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    public string lvlName;
    public bool success;
    public bool[] fragments;
    public float time;

    public void Reset()
    {
        for (int i = 0; i < fragments.Length; i++)
        {
            fragments[i] = false;
        }
        time = 0;
        success = false;
    }

    public string ConvertTimeToTimer(float _t)
    {
        float t = Mathf.RoundToInt(_t * 100) * 0.01f;
        float m = Mathf.FloorToInt(t / 60);
        string m_string = (m == 0) ? "00" : m.ToString();
        m_string = (m < 10 && m > 0) ? "0" + m_string : m_string;
        float s = Mathf.FloorToInt(t % 60);
        string s_string = (s == 0) ? "00" : s.ToString();
        s_string = (s < 10 && s>0) ? "0" + s_string : s_string;
        float ms = Mathf.RoundToInt((t - s - m*60)*100);
        string ms_string = (ms == 0 && ms>0) ? "00" : ms.ToString();
        ms_string = (ms < 10) ? "0" + ms_string : ms_string;
        return "timer: " + m_string + ":" + s_string + ":" + ms_string;
    }
}
