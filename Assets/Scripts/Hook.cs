using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public string currentTowel; // The name of the towel currently on this hook

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object is a towel
        if (other.CompareTag("Towel"))
        {
            // If the hook is already occupied, ignore the new towel
            if (!string.IsNullOrEmpty(currentTowel))
            {
                Debug.Log($"Hook {gameObject.name} is already occupied by towel {currentTowel}. Ignoring {other.name}.");
                return;
            }

            // If the hook is free, set the current towel
            currentTowel = other.name;
            Debug.Log($"Towel {currentTowel} added to hook {gameObject.name}.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object is a towel
        if (other.CompareTag("Towel"))
        {
            // Remove the towel only if it matches the current towel
            if (currentTowel == other.name)
            {
                Debug.Log($"Towel {currentTowel} removed from hook {gameObject.name}.");
                currentTowel = null;
            }
        }
    }
}