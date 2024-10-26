using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        // chech, if the game was saved
        if (PlayerPrefs.HasKey("SavedScene"))
        {
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
        // If necessary, reset saves
        PlayerPrefs.DeleteKey("SavedScene");
        // Load the opening scene of the game
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void QuitGame()
    {
        // Exit the game
        Application.Quit();
        Debug.Log("Game closed");
    }
}
