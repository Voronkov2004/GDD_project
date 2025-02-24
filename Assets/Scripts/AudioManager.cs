using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;

    public AudioMixerGroup musicMixerGroup;

    void Start()
    {
        audioSource.outputAudioMixerGroup = musicMixerGroup;
    }


    void Awake()
    {
        Debug.Log("AudioManager Awake called");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("AudioManager instance set");
        }
        else
        {
            Debug.LogWarning("Duplicate AudioManager destroyed");
            Destroy(gameObject);
        }
    }

    public void PlayRandomClip(AudioClipGroup clipGroup)
    {
        if (clipGroup == null || clipGroup.Clips.Count == 0)
        {
            Debug.LogWarning("PlayRandomClip: Clip group is null or empty!");
            return;
        }

        AudioClip clip = clipGroup.Clips[Random.Range(0, clipGroup.Clips.Count)];
        audioSource.pitch = Random.Range(clipGroup.PitchMin, clipGroup.PitchMax);
        audioSource.volume = Random.Range(clipGroup.VolumeMin, clipGroup.VolumeMax);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(clip);
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

