using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioClip settingsMenuMusic;

    void Start()
    {
        Debug.Log("SettingsMenu Start called");
        if (AudioManager.instance != null)
        {
            Debug.Log("Calling PlayMusic from SettingsMenu");
            AudioManager.instance.PlayMusic(settingsMenuMusic);
        }
        else
        {
            Debug.LogError("AudioManager.instance is null in SettingsMenu");
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
