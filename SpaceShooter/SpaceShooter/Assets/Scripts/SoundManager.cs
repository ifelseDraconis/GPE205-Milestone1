using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Audio players components
    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    // Singleton Instance
    public static SoundManager thisSoundManager = null;

    private void Awake()
    {
        if(thisSoundManager == null)
        {
            thisSoundManager = this;
        }
        else if(thisSoundManager != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);       
    }

    public void Play(AudioClip clip, float thisVolume)
    {
        EffectsSource.volume = thisVolume;
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    public void PlayMusic(AudioClip clip, float thisVolume)
    {
        MusicSource.Stop();
        MusicSource.loop = true;
        MusicSource.clip = clip;
        MusicSource.volume = thisVolume;
        MusicSource.Play();
    }
}
