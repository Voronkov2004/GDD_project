using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuItemActivator : MonoBehaviour
{
    public GameObject note; // Reference to the note object in the scene
    public GameObject flashlight; // Reference to the flashlight object in the scene

    void Start()
    {
        // Check if the chest puzzle was solved (loaded from PlayerPrefs)
        if (GameStateManager.Instance != null && GameStateManager.Instance.isChestPuzzleSolved)
        {
            if (note != null)
            {
                note.SetActive(true);
                Debug.Log("Note activated in the main menu.");
            }

            if (flashlight != null)
            {
                flashlight.SetActive(true);
                Debug.Log("Flashlight activated in the main menu.");
            }
        }
        else
        {
            Debug.Log("Chest puzzle not solved. Items remain hidden.");
        }
    }
}
