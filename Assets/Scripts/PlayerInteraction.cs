using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI panelText;
    public GameObject keyImagePrefab;
    public Transform inventoryUI;
    public string kitchenSceneName;
    public string upstairsSceneName;
    public string downStairsSceneName;
    public string boardSceneName;

    private bool isInsideKeyTrigger = false;
    private bool isInsideKitchenTrigger = false;
    private bool isInsideLadderTrigger = false;
    private bool isInsideLadderDownTrigger = false;  
    private bool isInsideOutsideTrigger = false;   
    private bool isInsideBoardTrigger = false;
    private GameObject currentKey;

    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }

        // Rebuild inventory UI
        foreach (string tag in InventoryManager.Instance.collectedTags)
        {
            if (tag == "Key" && keyImagePrefab != null && inventoryUI != null)
            {
                Instantiate(keyImagePrefab, inventoryUI);
            }
        }
    }

    void Update()
    {
        if (isInsideKeyTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (currentKey != null && currentKey.CompareTag("Key"))
            {
                Destroy(currentKey);
                InventoryManager.Instance.AddItem("Key");

                if (keyImagePrefab != null && inventoryUI != null)
                {
                    Instantiate(keyImagePrefab, inventoryUI);
                }
                panel.SetActive(false);
            }
        }

        if (isInsideKitchenTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (StateManager.kitchenUnlocked) // Check if the kitchen has already been unlocked
            {
                SceneManager.LoadScene(kitchenSceneName);
            }
            else
            {
                if (InventoryManager.Instance.HasItem("Key"))
                {
                    if (inventoryUI.childCount > 0)
                    {
                        Transform lastKey = inventoryUI.GetChild(inventoryUI.childCount - 1);
                        Destroy(lastKey.gameObject);
                    }
                    InventoryManager.Instance.RemoveItem("Key");
                    StateManager.kitchenUnlocked = true;
                    SceneManager.LoadScene(kitchenSceneName);
                }
                else
                {
                    // Show message if the key is missing
                    panelText.text = "You need a key to unlock this door!";
                    panel.SetActive(true);
                }
            }
        }

        if (isInsideLadderTrigger && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(upstairsSceneName);
        }

        if (isInsideLadderDownTrigger && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(downStairsSceneName);
        }

        if (isInsideOutsideTrigger && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("GameScene");
        }

        if (isInsideBoardTrigger && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(boardSceneName);
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
                panelText.text = StateManager.kitchenUnlocked ? "Press F to enter" : "Press F to unlock the door";
                panel.SetActive(true);
            }
        }
        else if (collision.CompareTag("Ladder"))
        {
            isInsideLadderTrigger = true;
            if (panel != null)
            {
                panel.SetActive(true);
            }
        }
        else if (collision.CompareTag("LadderDown"))
        {
            isInsideLadderDownTrigger = true;
            if (panel != null)
            {
                panel.SetActive(true);
            }
        }

        else if (collision.CompareTag("Outside"))
        {
            isInsideOutsideTrigger = true;
            if (panel != null)
            {
                panel.SetActive(true);
            }
        }

        else if (collision.CompareTag("Board"))
        {
            isInsideBoardTrigger = true;
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
                panel.SetActive(false);
                panelText.text = "Press F to interact";
            }
        }
        else if (collision.CompareTag("Ladder"))
        {
            isInsideLadderTrigger = false;
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }
        else if (collision.CompareTag("LadderDown"))
        {
            isInsideLadderDownTrigger = false;
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }

        else if (collision.CompareTag("Outside"))
        {
            isInsideOutsideTrigger = false;
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }

        else if (collision.CompareTag("Board"))
        {
            isInsideOutsideTrigger = false;
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }
    }
}
