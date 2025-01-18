using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AudioClip mainMenuMusic;

    void Start()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(mainMenuMusic);
            if (mainMenuMusic == null)
            {
                Debug.LogError("MainMenuMusic is null!");
            }
        }
        else
        {
            Debug.LogError("AudioManager.instance is null in MainMenu");
        }
    }

    public void ContinueGame()
    {
        string savedScene = GameStateManager.Instance.GetSavedScene();

        if (!string.IsNullOrEmpty(savedScene))
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.StopMusic();
            }

            // Load the saved scene
            SceneManager.LoadScene(savedScene);
            Debug.Log($"Continuing game from scene: {savedScene}");
        }
        else
        {
            Debug.Log("No saved game found!");
        }
    }


    public void NewGame()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopMusic();
        }

        // If necessary, reset saves
        GameStateManager.Instance.ResetProgress();

        // Load the opening scene of the game
        SceneManager.LoadScene("IntroScene");
        Debug.Log("Starting a new game...");
    }

    public void QuitGame()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopMusic();
        }

        // Exit the game
        Application.Quit();
        Debug.Log("Game closed");
    }


    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }
}
