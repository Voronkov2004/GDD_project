using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    //Audio
    public AudioSource audioSource;
    public AudioClip openDoorSound;
    public AudioClip closedDoorSound;
    public AudioClip itemPickupSound;
    public AudioClip notePickupSound;
    public AudioClip batteryPickupSound;
    public AudioClip flashlightPickupSound;



    // UI and Inventory
    public GameObject panel;
    public TextMeshProUGUI panelText;
    public GameObject keyImagePrefab;
    public GameObject flashlightImagePrefab;
    public GameObject batteryImagePrefab;
    public GameObject combinedFlashlightImagePrefab;
    public Transform inventoryUI;
    public GameObject notePanel;
    public TextMeshProUGUI noteTextUI;

    // Scene Names
    public string kitchenSceneName;  
    public string upstairsSceneName;
    public string downStairsSceneName;
    public string boardSceneName;       // Set this to your static scene's name ("Board")
    public string toiletSceneName;

    // Trigger Flags
    private bool isInsideKeyTrigger = false;
    private bool isInsideFlashlightTrigger = false;
    private bool isInsideBatteryTrigger = false;
    private bool isInsideKitchenTrigger = false;
    private bool isInsideLadderTrigger = false;
    private bool isInsideLadderDownTrigger = false;
    private bool isInsideOutsideTrigger = false;
    private bool isInsideBoardTrigger = false; // Added for "Board" scene
    private bool isInsideToiletTrigger = false;
    private bool isInsideNoteTrigger = false;
    private bool isNoteOpen = false;

    // Current Objects
    private GameObject currentKey;
    private GameObject currentFlashlight;
    private GameObject currentBattery;
    private GameObject currentNote;

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

        if (notePanel != null)
        {
            notePanel.SetActive(false);
        }

        // Restore player's position
        RestorePlayerPosition();

        // Initialize inventory UI
        foreach (KeyValuePair<string, int> entry in InventoryManager.Instance.collectedItems)
        {
            string tag = entry.Key;
            int count = entry.Value;

            for (int i = 0; i < count; i++)
            {
                if (tag == "Key" && keyImagePrefab != null && inventoryUI != null)
                {
                    GameObject keyIcon = Instantiate(keyImagePrefab, inventoryUI);
                    keyIcon.name = "Key";
                }
                else if (tag == "Flashlight1" && flashlightImagePrefab != null && inventoryUI != null)
                {
                    GameObject flashlightIcon = Instantiate(flashlightImagePrefab, inventoryUI);
                    flashlightIcon.name = "Flashlight1";
                }
                else if (tag == "Battery" && batteryImagePrefab != null && inventoryUI != null)
                {
                    GameObject batteryIcon = Instantiate(batteryImagePrefab, inventoryUI);
                    batteryIcon.name = "Battery";
                }
                else if (tag == "CombinedFlashlight" && combinedFlashlightImagePrefab != null && inventoryUI != null)
                {
                    GameObject combinedIcon = Instantiate(combinedFlashlightImagePrefab, inventoryUI);
                    combinedIcon.name = "CombinedFlashlight";
                }
            }
        }
    }

    void Update()
    {
        HandleKeyInteraction();
        HandleFlashlightInteraction();
        HandleBatteryInteraction();
        HandleSceneTransitions();
        HandleNoteInteraction();

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
                audioSource.PlayOneShot(itemPickupSound);
                Destroy(currentKey);
                InventoryManager.Instance.AddItem("Key");

                if (keyImagePrefab != null && inventoryUI != null)
                {
                    GameObject keyIcon = Instantiate(keyImagePrefab, inventoryUI);
                    keyIcon.name = "Key";
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
                if (audioSource != null && flashlightPickupSound != null)
                {
                    audioSource.PlayOneShot(flashlightPickupSound);
                }

                Destroy(currentFlashlight);
                InventoryManager.Instance.AddItem("Flashlight1");

                if (flashlightImagePrefab != null && inventoryUI != null)
                {
                    GameObject flashlightIcon = Instantiate(flashlightImagePrefab, inventoryUI);
                    flashlightIcon.name = "Flashlight1";
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

                if (audioSource != null && batteryPickupSound != null)
                {
                    audioSource.PlayOneShot(batteryPickupSound);
                }

                Destroy(currentBattery);
                InventoryManager.Instance.AddItem("Battery");

                if (batteryImagePrefab != null && inventoryUI != null)
                {
                    GameObject batteryIcon = Instantiate(batteryImagePrefab, inventoryUI);
                    batteryIcon.name = "Battery";
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

        // Remove one flashlight and one battery icon from the inventory UI
        RemoveItemIconFromUI("Flashlight1");
        RemoveItemIconFromUI("Battery");

        InventoryManager.Instance.AddItem("CombinedFlashlight");

        if (combinedFlashlightImagePrefab != null && inventoryUI != null)
        {
            GameObject combinedIcon = Instantiate(combinedFlashlightImagePrefab, inventoryUI);
            combinedIcon.name = "CombinedFlashlight";
        }

        Debug.Log("Flashlight and battery combined into a single item. Inventory updated.");
    }

    private void RemoveItemIconFromUI(string itemName)
    {
        foreach (Transform child in inventoryUI)
        {
            if (child.name == itemName)
            {
                Destroy(child.gameObject);
                break; // Remove only one icon
            }
        }
    }

    private void HandleSceneTransitions()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isInsideKitchenTrigger)
            {
                SaveCurrentPlayerPosition();

                if (StateManager.kitchenUnlocked)
                {
                    if (audioSource != null && openDoorSound != null)
                    {
                        audioSource.PlayOneShot(openDoorSound);
                    }
                    SceneManager.LoadScene(kitchenSceneName);
                }
                else if (InventoryManager.Instance.HasItem("Key"))
                {
                    InventoryManager.Instance.RemoveItem("Key");
                    if (audioSource != null && openDoorSound != null)
                    {
                        audioSource.PlayOneShot(openDoorSound);
                    }

                    // Remove one key icon from the inventory UI
                    RemoveItemIconFromUI("Key");

                    StateManager.kitchenUnlocked = true;
                    SceneManager.LoadScene(kitchenSceneName);
                }
                else
                {
                    audioSource.PlayOneShot(closedDoorSound);
                    panelText.text = "You need a key to unlock this door!";
                    panel.SetActive(true);
                }
            }
            else if (isInsideLadderTrigger)
            {
                SaveCurrentPlayerPosition();
                SceneManager.LoadScene(upstairsSceneName);
            }
            else if (isInsideLadderDownTrigger)
            {
                SaveCurrentPlayerPosition();
                SceneManager.LoadScene(downStairsSceneName);
            }
            else if (isInsideOutsideTrigger)
            {
                SaveCurrentPlayerPosition();
                SceneManager.LoadScene("GameScene");
            }
            else if (isInsideBoardTrigger)
            {
                SaveCurrentPlayerPosition();
                SceneManager.LoadScene(boardSceneName); // Transition to "Board" scene
            }
            else if (isInsideToiletTrigger)
            {
                SaveCurrentPlayerPosition();
                SceneManager.LoadScene(toiletSceneName);
            }
        }
    }

    private void SaveCurrentPlayerPosition()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Vector3 playerPosition = transform.position;

        InventoryManager.Instance.SavePlayerPosition(currentSceneName, playerPosition);
    }

    private void RestorePlayerPosition()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Vector3? savedPosition = InventoryManager.Instance.GetPlayerPosition(currentSceneName);

        if (savedPosition.HasValue)
        {
            transform.position = savedPosition.Value;
            Debug.Log("Restored player position in " + currentSceneName + ": " + savedPosition.Value);
        }
        else
        {
            // Optional: Set a default position if no saved position exists
            // transform.position = new Vector3(0, 0, 0); // Replace with your desired default position
            Debug.Log("No saved position for " + currentSceneName + ". Using default position.");
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
        else if (collision.CompareTag("Note"))
        {
            isInsideNoteTrigger = true;
            currentNote = collision.gameObject;
            panelText.text = "Press F to read the note. Press F again to close it.";
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
        else if (collision.CompareTag("Note"))
        {
            isInsideNoteTrigger = false;
            currentNote = null;
            panel?.SetActive(false);
        }

        panel?.SetActive(false);
    }

    private void HandleNoteInteraction()
    {
        if (isNoteOpen)
        {
            // If the note is open, check if you press F or Escape to close it
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape))
            {
                CloseNoteUI();
                Time.timeScale = 1f; // resume the game
                isNoteOpen = false;
            }
        }
        else if (isInsideNoteTrigger && Input.GetKeyDown(KeyCode.F))
        {
            // If the note is not open and we are in the trigger, we open the note
            if (currentNote != null && currentNote.CompareTag("Note"))
            {
                Note noteComponent = currentNote.GetComponent<Note>();
                if (noteComponent != null)
                {
                    OpenNoteUI(noteComponent.noteText);
                    Time.timeScale = 0f; // stop the game
                    isNoteOpen = true;
                    panel?.SetActive(false); // hide the hint


                    if (notePickupSound != null)
                    {
                        audioSource.PlayOneShot(notePickupSound);
                    }
                }
            }
        }
    }

    private void OpenNoteUI(string text)
    {
        if (notePanel != null && noteTextUI != null)
        {
            notePanel.SetActive(true);
            noteTextUI.text = text;
        }
    }

    private void CloseNoteUI()
    {
        if (notePanel != null)
        {
            notePanel.SetActive(false);
        }
    }
}
