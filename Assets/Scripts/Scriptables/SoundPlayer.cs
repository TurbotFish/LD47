using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public List<AudioClip> clips = new List<AudioClip>();

    public bool randomPitch;
    public float pitchRandom = 0.01f;
    float pitch;
    private void Awake()
    {
        pitch = audioSource.pitch;
    }

    public void PlaySound(int clipID)
    {
        audioSource.clip = clips[clipID];
        if(audioSource.isActiveAndEnabled)
        {
            if (randomPitch)
            {
                float p = Random.Range(-pitchRandom, pitchRandom);
                audioSource.pitch = pitch + p;
            }
            audioSource.Play();
        }
    }
}
