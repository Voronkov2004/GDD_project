using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;

    void Awake()
    {
        Debug.Log("AudioManager Awake called");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("AudioManager instance set");
        }
        else
        {
            Debug.LogWarning("Duplicate AudioManager destroyed");
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicClip == null)
        {
            Debug.LogError("PlayMusic: musicClip is null!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("PlayMusic: audioSource is null!");
            return;
        }

        if (audioSource.clip != musicClip)
        {
            Debug.Log("Assigning new clip: " + musicClip.name);
            audioSource.Stop();
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Music started: " + musicClip.name);
        }
        else
        {
            Debug.Log("Music already playing: " + musicClip.name);
        }
    }



    public void StopMusic()
    {
        audioSource.Stop();
    }
}

