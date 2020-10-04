using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MuteAudioGroup : MonoBehaviour
{
    public AudioMixerGroup amg;
    float v;

    private void Awake()
    {
       amg.audioMixer.GetFloat("SoundVolume", out v);    
    }

    public void GroupOn(bool b)
    {
        if (b)
        {
            amg.audioMixer.SetFloat("SoundVolume", v);
        }
        else
        {
            amg.audioMixer.SetFloat("SoundVolume", -80f);
        }
    }
}
