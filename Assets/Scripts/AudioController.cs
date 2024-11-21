using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioMixer audioMixer; 
    public Slider soundSlider; 
    public Slider musicSlider; 

    void Start()
    {
        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);

        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 50);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 50);
    }

    public void SetSoundVolume(float value)
    {
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(value / 100) * 20);
        PlayerPrefs.SetFloat("SoundVolume", value); 
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value / 100) * 20);
        PlayerPrefs.SetFloat("MusicVolume", value); 
    }
}

