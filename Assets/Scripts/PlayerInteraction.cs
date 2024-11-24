using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI panelText;
    public GameObject keyImagePrefab;
    public GameObject flashlightImagePrefab;
    public GameObject batteryImagePrefab;
    public GameObject combinedFlashlightImagePrefab;
    public Transform inventoryUI;
    public string kitchenSceneName;
    public string upstairsSceneName;
    public string downStairsSceneName;
    public string boardSceneName;
    public string toiletSceneName;

    private bool isInsideKeyTrigger = false;
    private bool isInsideFlashlightTrigger = false;
    private bool isInsideBatteryTrigger = false;
    private bool isInsideKitchenTrigger = false;
    private bool isInsideLadderTrigger = false;
    private bool isInsideLadderDownTrigger = false;
    private bool isInsideOutsideTrigger = false;
    private bool isInsideBoardTrigger = false;
    private bool isInsideToiletTrigger = false;

    private GameObject currentKey;
    private GameObject currentFlashlight;
    private GameObject currentBattery;

    void Start()
    {
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager.Instance is null. Ensure the InventoryManager exists in the scene.");
            return;
        }

        if (panel != null)
        {
            panel.SetActive(false);
        }

        foreach (string tag in InventoryManager.Instance.collectedTags)
        {
            if (tag == "Key" && keyImagePrefab != null && inventoryUI != null)
            {
                Instantiate(keyImagePrefab, inventoryUI);
            }
            else if (tag == "Flashlight1" && flashlightImagePrefab != null && inventoryUI != null)
            {
                Instantiate(flashlightImagePrefab, inventoryUI);
            }
            else if (tag == "Battery" && batteryImagePrefab != null && inventoryUI != null)
            {
                Instantiate(batteryImagePrefab, inventoryUI);
            }
            else if (tag == "CombinedFlashlight" && combinedFlashlightImagePrefab != null && inventoryUI != null)
            {
                Instantiate(combinedFlashlightImagePrefab, inventoryUI);
            }
        }
    }

    void Update()
    {
        HandleKeyInteraction();
        HandleFlashlightInteraction();
        HandleBatteryInteraction();
        HandleSceneTransitions();

        if (InventoryManager.Instance.HasItem("Flashlight1") && InventoryManager.Instance.HasItem("Battery"))
        {
            CombineFlashlightAndBattery();
        }
    }

    private void HandleKeyInteraction()
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
    }

    private void HandleFlashlightInteraction()
    {
        if (isInsideFlashlightTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (currentFlashlight != null && currentFlashlight.CompareTag("Flashlight1"))
            {
                Destroy(currentFlashlight);
                InventoryManager.Instance.AddItem("Flashlight1");

                if (flashlightImagePrefab != null && inventoryUI != null)
                {
                    Instantiate(flashlightImagePrefab, inventoryUI);
                }

                panel.SetActive(false);
            }
        }
    }

    private void HandleBatteryInteraction()
    {
        if (isInsideBatteryTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (currentBattery != null && currentBattery.CompareTag("Battery"))
            {
                Destroy(currentBattery);
                InventoryManager.Instance.AddItem("Battery");

                if (batteryImagePrefab != null && inventoryUI != null)
                {
                    Instantiate(batteryImagePrefab, inventoryUI);
                }

                panel.SetActive(false);
            }
        }
    }

private void CombineFlashlightAndBattery()
{
    if (InventoryManager.Instance.HasItem("CombinedFlashlight"))
    {
        Debug.Log("Combined flashlight already exists in inventory.");
        return;
    }

    InventoryManager.Instance.RemoveItem("Flashlight1");
    InventoryManager.Instance.RemoveItem("Battery");

    List<GameObject> itemsToRemove = new List<GameObject>();
    foreach (Transform child in inventoryUI)
    {
        if (child.name.Contains("Flashlight1") || child.name.Contains("Battery"))
        {
            itemsToRemove.Add(child.gameObject);
        }
    }

    foreach (GameObject item in itemsToRemove)
    {
        Destroy(item);
    }

    InventoryManager.Instance.AddItem("CombinedFlashlight");

    if (combinedFlashlightImagePrefab != null && inventoryUI != null)
    {
        GameObject combinedIcon = Instantiate(combinedFlashlightImagePrefab, inventoryUI);
        combinedIcon.name = "CombinedFlashlight";
    }

    Debug.Log("Flashlight and battery combined into a single item. Inventory updated.");
}


    private void HandleSceneTransitions()
    {
        if (isInsideKitchenTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (StateManager.kitchenUnlocked)
            {
                SceneManager.LoadScene(kitchenSceneName);
            }
            else if (InventoryManager.Instance.HasItem("Key"))
            {
                InventoryManager.Instance.RemoveItem("Key");
                StateManager.kitchenUnlocked = true;

                foreach (Transform child in inventoryUI)
                {
                    if (child.name == "Key")
                    {
                        Destroy(child.gameObject);
                        break;
                    }
                }

                SceneManager.LoadScene(kitchenSceneName);
            }
            else
            {
                panelText.text = "You need a key to unlock this door!";
                panel.SetActive(true);
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

        if (isInsideToiletTrigger && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(toiletSceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            isInsideKeyTrigger = true;
            currentKey = collision.gameObject;
            panelText.text = "Press F to pick up the key!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Flashlight1"))
        {
            isInsideFlashlightTrigger = true;
            currentFlashlight = collision.gameObject;
            panelText.text = "Press F to pick up the flashlight!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Battery"))
        {
            isInsideBatteryTrigger = true;
            currentBattery = collision.gameObject;
            panelText.text = "Press F to pick up the battery!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Kitchen"))
        {
            isInsideKitchenTrigger = true;
            panelText.text = StateManager.kitchenUnlocked ? "Press F to enter" : "Press F to unlock the door";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Ladder"))
        {
            isInsideLadderTrigger = true;
            panelText.text = "Press F to climb the ladder!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("LadderDown"))
        {
            isInsideLadderDownTrigger = true;
            panelText.text = "Press F to go downstairs!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Outside"))
        {
            isInsideOutsideTrigger = true;
            panelText.text = "Press F to go outside!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Board"))
        {
            isInsideBoardTrigger = true;
            panelText.text = "Press F to view the board!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Toilet"))
        {
            isInsideToiletTrigger = true;
            panelText.text = "Press F to enter the toilet!";
            panel?.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            isInsideKeyTrigger = false;
            currentKey = null;
        }
        else if (collision.CompareTag("Flashlight1"))
        {
            isInsideFlashlightTrigger = false;
            currentFlashlight = null;
        }
        else if (collision.CompareTag("Battery"))
        {
            isInsideBatteryTrigger = false;
            currentBattery = null;
        }
        else if (collision.CompareTag("Kitchen"))
        {
            isInsideKitchenTrigger = false;
        }
        else if (collision.CompareTag("Ladder"))
        {
            isInsideLadderTrigger = false;
        }
        else if (collision.CompareTag("LadderDown"))
        {
            isInsideLadderDownTrigger = false;
        }
        else if (collision.CompareTag("Outside"))
        {
            isInsideOutsideTrigger = false;
        }
        else if (collision.CompareTag("Board"))
        {
            isInsideBoardTrigger = false;
        }
        else if (collision.CompareTag("Toilet"))
        {
            isInsideToiletTrigger = false;
        }

        panel?.SetActive(false);
    }
}
