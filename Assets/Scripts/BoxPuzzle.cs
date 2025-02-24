using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BoxPuzzle : MonoBehaviour
{
    public string correctCode = "123"; // Correct numeric code
    public TMP_Text progressText; // Text to show the player's current progress
    public TMP_Text exitHintText; // Text to show the exit hint

    public AudioSource audioSource; // Audio source for playing sounds
    public AudioClip cupboardOpenSound;

    private bool isHintVisible = false; // Flag to control hint visibility

    private string currentInput = ""; // Player's current input

    void Start()
    {
        // Initialize the progress display
        UpdateProgressText();

        // Hide exit hint at the start
        if (exitHintText != null)
        {
            exitHintText.text = "Press F to return to the library.";
            exitHintText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Allow player to return to the library
        if (Input.GetKeyDown(KeyCode.F))
        {
            ReturnToLibrary();
        }
        // Show the exit hint after a short delay if not already visible
        if (!isHintVisible)
        {
            StartCoroutine(ShowExitHint());
        }
    }

    public void EnterNumber(string number)
    {
        // Add the number if the input is not yet complete
        if (currentInput.Length < correctCode.Length)
        {
            currentInput += number;

            // Update the progress display
            UpdateProgressText();

            // Check if the code is complete
            if (currentInput.Length == correctCode.Length)
            {
                CheckInput();
            }
        }
    }

    private void CheckInput()
    {
        if (currentInput == correctCode)
        {
            // Correct input: open the chest
            Debug.Log("Correct! The chest is opening...");

            audioSource.PlayOneShot(cupboardOpenSound);

            // Update the game state
            GameStateManager.Instance.isChestPuzzleSolved = true;

            // Save progress
            GameStateManager.Instance.SaveProgress();

            // Load the next scene after a delay
            Invoke("ReturnToLibrary", 2f);
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
        for (int i = 0; i < correctCode.Length; i++)
        {
            if (i < currentInput.Length)
            {
                display += " " + currentInput[i];
            }
            else
            {
                display += " _";
            }

            if (i < correctCode.Length - 1)
            {
                display += " ";
            }
        }
        progressText.text = display;
    }

    private void ReturnToLibrary()
    {
        SceneManager.LoadScene("Library");
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
