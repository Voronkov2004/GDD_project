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
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.StopMusic();
            }

            string sceneName = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("No saved game");
        }
    }


    public void NewGame()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopMusic();
        }

        // If necessary, reset saves
        PlayerPrefs.DeleteKey("SavedScene");
        // Load the opening scene of the game
        SceneManager.LoadScene("GameScene");
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
