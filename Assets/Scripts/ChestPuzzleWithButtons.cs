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
    public TMP_Text feedbackText; // Feedback message text
    //public GameObject chestClosed; // Closed chest object
    //public GameObject chestOpen; // Open chest object

    private string currentInput = ""; // Player's current input

    void Start()
    {
        // Initialize the progress display and chest states
        UpdateProgressText();
        feedbackText.text = "Guess the camp name!";
        //chestClosed.SetActive(true);
        //chestOpen.SetActive(false);
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
            feedbackText.text = "Correct! The chest is opening!";
            //chestClosed.SetActive(false);
            //chestOpen.SetActive(true);

            // Load the next scene after a delay
            Invoke("LoadOpenChestScene", 2f);
        }
        else
        {
            // Incorrect input: reset the progress
            Debug.Log("Incorrect! Try again.");
            feedbackText.text = "Incorrect! Try again.";
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
                display += currentInput[i];
            }
            else
            {
                display += "_";
            }

            if (i < correctName.Length - 1)
            {
                display += " ";
            }
        }
        progressText.text = display;
    }

    private void LoadOpenChestScene()
    {
        SceneManager.LoadScene("Kitchen"); // Replace with your scene name
    }
}

