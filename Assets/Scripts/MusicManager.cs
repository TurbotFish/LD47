using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource source;
    public List<AudioClip> musics;
    int m;
    float vol;
    private void Awake()
    {
        vol = source.volume;
    }

    public void MusicOn(bool b)
    {
        if(b)
        {
            source.volume = vol;
        }
        else
        {
            source.volume = 0;
        }
    }
    public void ChangeMusic(int id)
    {
        if (id == m)
        {
            return;
        }
        else if (id<musics.Count)
        {
            m = id;
            source.clip = musics[id];
            source.Play();
        }
    }
}
