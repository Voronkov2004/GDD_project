using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this namespace

public class PlayerInteraction : MonoBehaviour
{
    public GameObject panel;
    public GameObject keyImagePrefab;
    public Transform inventoryUI;
    public string roomSceneName; // Reference to the room scene name

    private bool isInsideKeyTrigger = false;
    private bool isInsideKitchenTrigger = false;
    private GameObject currentKey;

    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        if (isInsideKeyTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (currentKey != null)
            {
                Destroy(currentKey);
                if (keyImagePrefab != null && inventoryUI != null)
                {
                    Instantiate(keyImagePrefab, inventoryUI);
                }
                panel.SetActive(false);
            }
        }

        if (isInsideKitchenTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (inventoryUI.childCount > 0)
            {
                Transform lastKey = inventoryUI.GetChild(inventoryUI.childCount - 1);
                Destroy(lastKey.gameObject);
            }
            SceneManager.LoadScene(roomSceneName);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
                panel.SetActive(true);
            }
        }
        else if (collision.CompareTag("Kitchen"))
        {
            isInsideKitchenTrigger = true;
            if (panel != null)
            {
                panel.SetActive(true);
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
                panel.SetActive(false);
            }
        }
        else if (collision.CompareTag("Kitchen"))
        {
            isInsideKitchenTrigger = false;
            if (panel != null)
            {
                panel.SetActive(true);
            }
        }
    }
}