using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClosedChestController2 : MonoBehaviour
{
    public AudioSource audioSource; // Audio source for playing sounds
    public AudioClip chainsSound;

    public TMP_InputField codeInputField; 
    public TextMeshProUGUI hintText;

    private string correctCode = "3708";

    public void CheckCode()
    {
        string enteredCode = codeInputField.text;

        if (enteredCode == correctCode)
        {
            Debug.Log($"Correct!");

            StartCoroutine(HandleCorrectCode3708());
        }
        else
        {
            Debug.Log($"Incorrect!");
            hintText.text = "Incorrect code. Try again.";
        }
    }

    private IEnumerator HandleCorrectCode3708()
    {

        if (chainsSound != null){
        audioSource.PlayOneShot(chainsSound);

        yield return new WaitForSecondsRealtime(chainsSound.length);
        }
        GameStateManager.Instance.isStorage2Solved = true;

        SceneManager.LoadScene("OpenedCupboardScene2");
    }
}

