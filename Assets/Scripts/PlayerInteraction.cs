using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject panel; // Reference to the panel
    public GameObject keyImagePrefab; // Reference to the key image prefab
    public Transform inventoryUI; // Reference to the inventory UI

    private bool isInsideKeyTrigger = false; // Track if the player is inside the key's trigger radius
    private GameObject currentKey; // Reference to the current key GameObject

    // Start is called before the first frame update
    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Ensure the panel is initially hidden
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInsideKeyTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (currentKey != null)
            {
                Destroy(currentKey); // Make the key disappear from the game
                if (keyImagePrefab != null && inventoryUI != null)
                {
                    Instantiate(keyImagePrefab, inventoryUI); // Add the key image to the inventory UI
                }
                panel.SetActive(false); // Hide the panel
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            isInsideKeyTrigger = true;
            currentKey = collision.gameObject;
            if (panel != null)
            {
                panel.SetActive(true); // Make the panel visible
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            isInsideKeyTrigger = false;
            currentKey = null;
            if (panel != null)
            {
                panel.SetActive(false); // Hide the panel
            }
        }
    }
}