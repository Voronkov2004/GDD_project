using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClosedChestController : MonoBehaviour
{
    public TMP_InputField codeInputField; 
    public TextMeshProUGUI hintText;

    private string correctCode = "752";

    public void CheckCode()
    {
        string enteredCode = codeInputField.text;

        if (enteredCode == correctCode)
        {
            Debug.Log($"Correct!");

            GameStateManager.Instance.isStorageSolved = true;

            SceneManager.LoadScene("OpenedCupboardScene");
        }
        else
        {
            Debug.Log($"Incorrect!");
            hintText.text = "Incorrect code. Try again.";
        }
    }
}

