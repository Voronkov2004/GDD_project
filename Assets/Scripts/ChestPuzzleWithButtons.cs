using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ChestPuzzleWithButtons : MonoBehaviour
{
    public string correctName = "CAMPFIRE"; // Correct camp name
    public TMP_Text progressText; // Text to show the player's current progress
    public TMP_Text exitHintText; // Text to show the exit hint

    public AudioSource audioSource; // Audio source for playing sounds
    public AudioClip chestOpenSound;

    private string currentInput = ""; // Player's current input
    private bool isHintVisible = false; // Flag to control hint visibility

    void Start()
    {
        // Initialize the progress display and chest states
        UpdateProgressText();

        // Hide exit hint at the start
        if (exitHintText != null)
        {
            exitHintText.text = "Press F to return to the kitchen.";
            exitHintText.gameObject.SetActive(false);
        }

    }

    void Update()
    {
        // Allow player to return to the kitchen
        if (Input.GetKeyDown(KeyCode.F))
        {
            ReturnToKitchen();
        }

        // Show the exit hint after a short delay if not already visible
        if (!isHintVisible)
        {
            StartCoroutine(ShowExitHint());
        }
    }

    public void EnterLetter(string letter)
    {
        // Add the letter if the input is not yet complete
        if (currentInput.Length < correctName.Length)
        {
            currentInput += letter;

            // Update the progress display
            UpdateProgressText();

            // Check if the word is complete
            if (currentInput.Length == correctName.Length)
            {
                CheckInput();
            }
        }
    }

    private void CheckInput()
    {
        if (currentInput == correctName)
        {
            // Correct input: open the chest
            Debug.Log("Correct! The chest is opening...");

            audioSource.PlayOneShot(chestOpenSound);

            // Update the game state
            GameStateManager.Instance.isChestPuzzleSolved = true;

            // Save progress
            GameStateManager.Instance.SaveProgress();

            // Load the next scene after a delay
            Invoke("ReturnToKitchen", 2f);
        }
        else
        {
            // Incorrect input: reset the progress
            Debug.Log("Incorrect! Try again.");
            currentInput = "";
            UpdateProgressText();
        }
    }

    private void UpdateProgressText()
    {
        // Update the progress text with current input and underscores
        string display = "";
        for (int i = 0; i < correctName.Length; i++)
        {
            if (i < currentInput.Length)
            {
                display += " " + currentInput[i];
            }
            else
            {
                display += " _";
            }

            if (i < correctName.Length - 1)
            {
                display += " ";
            }
        }
        progressText.text = display;
    }

    private void ReturnToKitchen()
    {
        SceneManager.LoadScene("Kitchen"); 
    }

    private IEnumerator ShowExitHint()
    {
        yield return new WaitForSeconds(1f); // Wait 1 second before showing the hint

        if (exitHintText != null)
        {
            exitHintText.gameObject.SetActive(true);
            isHintVisible = true;
        }
    }
}

