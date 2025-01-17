using System.Collections;
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
    public GameObject keyKitchenLockerImagePrefab;
    public GameObject flashlightImagePrefab;
    public GameObject batteryImagePrefab;
    public GameObject combinedFlashlightImagePrefab;
    public GameObject boltCutterImagePrefab;
    public GameObject theatreKeyImagePrefab;
    public Transform inventoryUI;
    public GameObject notePanel;
    public TextMeshProUGUI noteTextUI;
    public GameObject lockerPicture;
    public GameObject lockerNote;
    public GameObject lockerTrigger;
    public GameObject closedLockerPanel;
    public GameObject openLockerPanel;
    public GameObject openedLockerInScene;

    // Scene Names
    public string kitchenSceneName;  
    public string upstairsSceneName;
    public string downStairsSceneName;
    public string boardSceneName;       // Set this to your static scene's name ("Board")
    public string toiletSceneName;
    public string chestPuzzleSceneName;
    public string towelsGameSceneName;
    public string theaterSceneName;
    public string storageSceneName;
    public string librarySceneName;


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
    private bool isInsideLockerTrigger = false;
    private bool isInsideBoltCutterTrigger = false;
    private bool isInsideLocker_ChainsTrigger = false;
    private bool isLockerInteractionActive = false;
    private bool isClosedLockerSceneOpen = false;
    private bool isInsideChestTrigger = false;
    private bool isInsideTowelsGameTrigger = false;
    private bool isTheaterTrigger = false;
    private bool isStorageTrigger = false;
    private bool isBackToTheaterTrigger = false;
    private bool isLibraryTrigger = false;
    private bool isInsideTentTrigger = false;
    private bool isRealKitchenTrigger = false;
    private bool isInsideTheatreKeyTrigger = false;


    // Current Objects
    private GameObject currentKey;
    private GameObject currentFlashlight;
    private GameObject currentBattery;
    private GameObject currentNote;
    private GameObject currentBoltCutter;
    private GameObject currentTheatreKey;

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

        // cabinet in toilet
        if (GameStateManager.Instance != null && GameStateManager.Instance.isOriginallyLockerOpened)
        {
            ActivateLockerContent();
        }
        else
        {
            DeactivateLockerContent();
        }

        if (GameStateManager.Instance.isLockerOpened) openedLockerInScene.SetActive(true);

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
                else if (tag == "KitchenLockerPrefab" && keyKitchenLockerImagePrefab != null && inventoryUI != null)
                {
                    GameObject kitchenLockerKey = Instantiate(keyKitchenLockerImagePrefab, inventoryUI);
                    kitchenLockerKey.name = "KitchenLockerPrefab";
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
                else if (tag == "BoltCutter" && boltCutterImagePrefab != null && inventoryUI != null)
                {
                    GameObject boltCutterIcon = Instantiate(boltCutterImagePrefab, inventoryUI);
                    boltCutterIcon.name = "BoltCutter";
                }
                else if (tag == "Keys_theatre_library" && theatreKeyImagePrefab != null && inventoryUI != null)
                {
                    GameObject boltCutterIcon = Instantiate(theatreKeyImagePrefab, inventoryUI);
                    boltCutterIcon.name = "Keys_theatre_library";
                }
            }
        }
    }

    void Update()
    {
        HandleItemPickup();
        HandleSceneTransitions();
        HandleNoteInteraction();

        if (InventoryManager.Instance.HasItem("Flashlight1") && InventoryManager.Instance.HasItem("Battery"))
        {
            CombineFlashlightAndBattery();
        }

        if (isInsideLockerTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (!GameStateManager.Instance.isOriginallyLockerOpened)
            {
                GameStateManager.Instance.isOriginallyLockerOpened = true;
                ActivateLockerContent();
                GameStateManager.Instance.SaveProgress();
            }
        }

        if (isInsideLocker_ChainsTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (isClosedLockerSceneOpen)
            {
                CloseClosedLockerUI();
            }
            else if (InventoryManager.Instance.HasItem("BoltCutter"))
            {
                // Bolt Cutter Scenario
                StartCoroutine(HandleLockerWithBoltCutter());
                //HandleLockerWithBoltCutter();
            }
            else
            {
                // Scenario without bolt cutters
                OpenClosedLockerUI();
            }
        }
    }

    private void HandleItemPickup()
    {
        if (isInsideKeyTrigger && Input.GetKeyDown(KeyCode.F))
        {
            audioSource.PlayOneShot(itemPickupSound);
            ProcessItemPickup(currentKey, "Key", keyImagePrefab);
        }
        else if (isInsideFlashlightTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (audioSource != null && flashlightPickupSound != null)
            {
                audioSource.PlayOneShot(flashlightPickupSound);
            }
            ProcessItemPickup(currentFlashlight, "Flashlight1", flashlightImagePrefab);
        }
        else if (isInsideBatteryTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (audioSource != null && batteryPickupSound != null)
            {
                audioSource.PlayOneShot(batteryPickupSound);
            }
            ProcessItemPickup(currentBattery, "Battery", batteryImagePrefab);
        }
        else if (isInsideBoltCutterTrigger && Input.GetKeyDown(KeyCode.F))
        {
            audioSource.PlayOneShot(itemPickupSound);
            ProcessItemPickup(currentBoltCutter, "BoltCutter", boltCutterImagePrefab);
        }
        else if (isInsideTheatreKeyTrigger && Input.GetKeyDown(KeyCode.F))
        {
            audioSource.PlayOneShot(itemPickupSound);
            ProcessItemPickup(currentTheatreKey, "Keys_theatre_library", theatreKeyImagePrefab);
        }
    }

    private void ProcessItemPickup(GameObject item, string defaultItemId, GameObject itemPrefab)
    {

        if (item != null)
        {
            string itemId = item.name;

            if (!GameStateManager.Instance.IsItemPickedUp(itemId))
            {
                // save that item was picked up
                GameStateManager.Instance.MarkItemPickedUp(itemId);
                InventoryManager.Instance.AddItem(defaultItemId);

                Destroy(item);

                if (itemPrefab != null && inventoryUI != null)
                {
                    GameObject itemIcon = Instantiate(itemPrefab, inventoryUI);
                    itemIcon.name = itemId;
                }

                Debug.Log($"Item {itemId} picked up and saved.");
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

                if (GameStateManager.Instance.isKitchenUnlocked) // StateManager.kitchenUnlocked
                {
                    if (audioSource != null && openDoorSound != null)
                    {
                        audioSource.PlayOneShot(openDoorSound);
                    }
                    LoadSceneWithSavedPosition(kitchenSceneName);
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

                    GameStateManager.Instance.isKitchenUnlocked = true; //StateManager.kitchenUnlocked = true;
                    LoadSceneWithSavedPosition(kitchenSceneName);
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
                LoadSceneWithSavedPosition(downStairsSceneName);
            }
            else if (isInsideTentTrigger)
            {
                if (GameStateManager.Instance.isTentLOpened)
                {
                    panelText.text = "TentL is already opened and cannot be accessed again.";
                    panel.SetActive(true);
                }
                else if (InventoryManager.Instance.HasItem("CombinedFlashlight"))
                {
                    SaveCurrentPlayerPosition();
                    GameStateManager.Instance.SaveProgress();
                    LoadSceneWithSavedPosition("TentL");
                    GameStateManager.Instance.isTentLOpened = true;
                }
                else
                {
                    SaveCurrentPlayerPosition();
                    LoadSceneWithSavedPosition("TentD");
                }
            }
            else if (isInsideOutsideTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition("GameScene");
            }
            else if (isBackToTheaterTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition(theaterSceneName);
            }
            else if (isRealKitchenTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition("RealKitchen");
            }
            else if (isTheaterTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition(theaterSceneName);
            }
            else if (isInsideBoardTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition(boardSceneName); // Transition to "Board" scene
            }
            else if (isInsideToiletTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition(toiletSceneName);
            }
            else if (isInsideChestTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition(chestPuzzleSceneName);
            }
            else if (isInsideTowelsGameTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition(towelsGameSceneName);
            }
            else if (isStorageTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition(storageSceneName);
            }
            else if (isLibraryTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition(librarySceneName);
            }
        }
    }

    private void SaveCurrentPlayerPosition()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Vector3 playerPosition = transform.position;

        //InventoryManager.Instance.SavePlayerPosition(currentSceneName, playerPosition);
        GameStateManager.Instance.SavePlayerLocation(currentSceneName, playerPosition);
    }

    private void LoadSceneWithSavedPosition(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        RestorePlayerPosition();
    }

    private void RestorePlayerPosition()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Vector3? savedPosition = GameStateManager.Instance.GetPlayerPosition(currentSceneName);

        if (savedPosition.HasValue)
        {
            transform.position = savedPosition.Value;
            Debug.Log($"Player position restored: {savedPosition.Value}");
        }
        else
        {
            Debug.Log($"No saved position for scene {currentSceneName}. Using default position.");
        }
    }

    //private void RestorePlayerPosition()
    //{
    //    string currentSceneName = SceneManager.GetActiveScene().name;
    //    Vector3? savedPosition = InventoryManager.Instance.GetPlayerPosition(currentSceneName);

    //    if (savedPosition.HasValue)
    //    {
    //        transform.position = savedPosition.Value;
    //        Debug.Log("Restored player position in " + currentSceneName + ": " + savedPosition.Value);
    //    }
    //    else
    //    {
    //        // Optional: Set a default position if no saved position exists
    //        // transform.position = new Vector3(0, 0, 0); // Replace with your desired default position
    //        Debug.Log("No saved position for " + currentSceneName + ". Using default position.");
    //    }
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collision detected with: {collision.gameObject.name}, Tag: {collision.tag}");
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
            panelText.text = GameStateManager.Instance.isKitchenUnlocked ? "Press F to enter" : "Press F to unlock the door"; // StateManager.kitchenUnlocked
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("RealKithcen"))
        {
            isRealKitchenTrigger = true;
            panelText.text = "Press F to enter the Kitchen!";
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
        else if (collision.CompareTag("Tent"))
        {
            isInsideTentTrigger = true;
            panelText.text = "Press F to view the tent!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Towels"))
        {
            isInsideTowelsGameTrigger = true;
            panelText.text = "Press F to view the towels!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Toilet"))
        {
            isInsideToiletTrigger = true;
            panelText.text = "Press F to enter the toilet!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Theater"))
        {
            isTheaterTrigger = true;
            panelText.text = "Press F to enter the theater!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("BackToTheater"))
        {
            isBackToTheaterTrigger = true;
            panelText.text = "Press F to enter the theater!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Storage"))
        {
            isStorageTrigger = true;
            panelText.text = "Press F to enter the theater storage room!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Library"))
        {
            isLibraryTrigger = true;
            panelText.text = "Press F to enter the library!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Note"))
        {
            isInsideNoteTrigger = true;
            currentNote = collision.gameObject;
            panelText.text = "Press F to read the note. Press F again to close it.";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Locker"))
        {
            isInsideLockerTrigger = true;
            panelText.text = "Press F to check the cabinet.";
            panel?.SetActive(true);
            Debug.Log("Player entered locker trigger area.");
        }
        else if (collision.CompareTag("BoltCutter"))
        {
            isInsideBoltCutterTrigger = true;
            currentBoltCutter = collision.gameObject;
            panelText.text = "Press F to pick up the bolt cutter!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Chains"))
        {
            isInsideLocker_ChainsTrigger = true;

            if (InventoryManager.Instance.HasItem("BoltCutter"))
            {
                panelText.text = "Press F to break the chains.";
            }
            else
            {
                panelText.text = "Press F to take a closer look.";
            }

            panel.SetActive(true);
        }
        else if (collision.CompareTag("Chest"))
        {
            isInsideChestTrigger = true;
            panelText.text = "Press F to inspect the chest.";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Keys_theatre_library"))
        {
            isInsideTheatreKeyTrigger = true;
            currentTheatreKey = collision.gameObject;
            panelText.text = "Press F to pick up the theatre key.";
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
        else if (collision.CompareTag("Library"))
        {
            isLibraryTrigger = false;
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
        else if (collision.CompareTag("RealKithcen"))
        {
            isRealKitchenTrigger = false;
        }
        else if (collision.CompareTag("Board"))
        {
            isInsideBoardTrigger = false;
        }
        else if (collision.CompareTag("Toilet"))
        {
            isInsideToiletTrigger = false;
        }
        else if (collision.CompareTag("Tent"))
        {
            isInsideTentTrigger = false;
        }
        else if (collision.CompareTag("Theater"))
        {
            isBackToTheaterTrigger = false;
            isTheaterTrigger = false;

        }
        else if (collision.CompareTag("Storage"))
        {
            isStorageTrigger = false;
        }
        else if (collision.CompareTag("BackToTheater"))
        {
            isTheaterTrigger = false;
        }
        else if (collision.CompareTag("Towels"))
        {
            isInsideTowelsGameTrigger = false;
            panel?.SetActive(false);
        }
        else if (collision.CompareTag("Note"))
        {
            isInsideNoteTrigger = false;
            currentNote = null;
            panel?.SetActive(false);
        }
        else if (collision.CompareTag("Locker"))
        {
            isInsideLockerTrigger = false;
            panel?.SetActive(false);
        }
        else if (collision.CompareTag("BoltCutter"))
        {
            isInsideBoltCutterTrigger = false;
            currentBoltCutter = null;
            panel?.SetActive(false);
        }
        else if (collision.CompareTag("Chains"))
        {
            isInsideLocker_ChainsTrigger = false;
            panel.SetActive(false);
        }
        else if (collision.CompareTag("Chest"))
        {
            isInsideChestTrigger = false;
            panel?.SetActive(false);
        }
        else if (collision.CompareTag("Keys_theatre_library"))
        {
            isInsideTheatreKeyTrigger = false;
            currentTheatreKey = null;
            panel?.SetActive(false);
        }

        panel?.SetActive(false);
    }

    private void HandleNoteInteraction()
    {
        if (isNoteOpen)
        {
            // If the note is open, check if player press F or Escape to close it
            if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape))
            {
                CloseNoteUI();
                Time.timeScale = 1f; // resume the game
                isNoteOpen = false;

                // Show message if defined in the Note
                if (currentNote != null && currentNote.CompareTag("Note"))
                {
                    Note noteComponent = currentNote.GetComponent<Note>();
                    if (noteComponent != null && !string.IsNullOrEmpty(noteComponent.closeNoteMessage))
                    {
                        StartCoroutine(ShowMessageSequence(noteComponent.closeNoteMessage, noteComponent.followUpMessage, 4f));
                    }
                }
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

    private IEnumerator ShowMessageSequence(string firstMessage, string secondMessage, float duration)
    {
        if (!string.IsNullOrEmpty(firstMessage))
        {
            // Show the first message
            if (panel != null && panelText != null)
            {
                panelText.text = firstMessage;
                panel.SetActive(true);

                yield return new WaitForSeconds(duration);

                panel.SetActive(false);
            }
        }

        if (!string.IsNullOrEmpty(secondMessage))
        {
            // Show the second message
            if (panel != null && panelText != null)
            {
                panelText.text = secondMessage;
                panel.SetActive(true);

                yield return new WaitForSeconds(duration);

                panel.SetActive(false);
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

    private void ActivateLockerContent()
    {
        if (lockerPicture != null)
        {
            lockerPicture.SetActive(true);
        }

        if (lockerNote != null)
        {
            lockerNote.SetActive(true);
        }

        if (lockerTrigger != null)
        {
            lockerTrigger.SetActive(false);
        }
    }

    private void DeactivateLockerContent()
    {
        if (lockerPicture != null)
        {
            lockerPicture.SetActive(false);
        }

        if (lockerNote != null)
        {
            lockerNote.SetActive(false);
        }

        if (lockerTrigger != null)
        {
            lockerTrigger.SetActive(true); 
        }
    }

    private IEnumerator HandleLockerWithBoltCutter()
    {
        if (isLockerInteractionActive) yield break;
        isLockerInteractionActive = true;

        ShowClosedLockerUI();

        // wait 2 sec
        yield return new WaitForSecondsRealtime(2f);

        closedLockerPanel.SetActive(false);
        openLockerPanel.SetActive(true);

        // wait 1 sec
        yield return new WaitForSecondsRealtime(1f);

        HideLockerUI();

        GameStateManager.Instance.isLockerOpened = true;

        openedLockerInScene.SetActive(true);

        InventoryManager.Instance.RemoveItem("BoltCutter");
        RemoveItemIconFromUI("BoltCutter");

        //GameStateManager.Instance.SaveProgress();

        DisableChainsTrigger();

        foreach (ItemSpawner spawner in FindObjectsOfType<ItemSpawner>())
        {
            spawner.UpdateItemSpawner();
        }

        foreach (ObjectActivator activator in FindObjectsOfType<ObjectActivator>())
        {
            activator.UpdateActivator();
        }


        isLockerInteractionActive = false;
    }

    private void DisableChainsTrigger()
    {
        var chainsTrigger = GameObject.FindWithTag("Chains");
        if (chainsTrigger != null)
        {
            chainsTrigger.SetActive(false);
            Debug.Log("Chains trigger has been disabled.");
        }
        else
        {
            Debug.LogWarning("Chains trigger not found!");
        }
    }


    private void ShowClosedLockerUI()
    {
        closedLockerPanel.SetActive(true);
        DisablePlayerMovement();

    }

    private void HideLockerUI()
    {
        closedLockerPanel.SetActive(false);
        openLockerPanel.SetActive(false);
        EnablePlayerMovement();

    }

    private void DisablePlayerMovement()
    {

        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = false;
        }
        else
        {
            Debug.LogWarning("PlayerMovement not found on MainCharacter!");
        }
    }

    private void EnablePlayerMovement()
    {

        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = true;
        }
        else
        {
            Debug.LogWarning("PlayerMovement not found on MainCharacter!");
        }
    }

    private void OpenClosedLockerUI()
    {
        if (closedLockerPanel != null)
        {
            closedLockerPanel.SetActive(true);
            DisablePlayerMovement();
            isClosedLockerSceneOpen = true;
        }
    }

    private void CloseClosedLockerUI()
    {
        if (closedLockerPanel != null)
        {
            closedLockerPanel.SetActive(false);
            EnablePlayerMovement();
            isClosedLockerSceneOpen = false;
        }
    }


    //private void HandleKeyInteraction()
    //{
    //    if (isInsideKeyTrigger && Input.GetKeyDown(KeyCode.F))
    //    {
    //        if (currentKey != null && currentKey.CompareTag("Key"))
    //        {
    //            audioSource.PlayOneShot(itemPickupSound);
    //            Destroy(currentKey);
    //            InventoryManager.Instance.AddItem("Key");

    //            if (keyImagePrefab != null && inventoryUI != null)
    //            {
    //                GameObject keyIcon = Instantiate(keyImagePrefab, inventoryUI);
    //                keyIcon.name = "Key";
    //            }

    //            panel.SetActive(false);
    //        }
    //    }
    //}

    //private void HandleFlashlightInteraction()
    //{
    //    if (isInsideFlashlightTrigger && Input.GetKeyDown(KeyCode.F))
    //    {
    //        if (currentFlashlight != null && currentFlashlight.CompareTag("Flashlight1"))
    //        {
    //            if (audioSource != null && flashlightPickupSound != null)
    //            {
    //                audioSource.PlayOneShot(flashlightPickupSound);
    //            }

    //            Destroy(currentFlashlight);
    //            InventoryManager.Instance.AddItem("Flashlight1");

    //            if (flashlightImagePrefab != null && inventoryUI != null)
    //            {
    //                GameObject flashlightIcon = Instantiate(flashlightImagePrefab, inventoryUI);
    //                flashlightIcon.name = "Flashlight1";
    //            }

    //            panel.SetActive(false);
    //        }
    //    }
    //}

    //private void HandleBatteryInteraction()
    //{
    //    if (isInsideBatteryTrigger && Input.GetKeyDown(KeyCode.F))
    //    {
    //        if (currentBattery != null && currentBattery.CompareTag("Battery"))
    //        {

    //            if (audioSource != null && batteryPickupSound != null)
    //            {
    //                audioSource.PlayOneShot(batteryPickupSound);
    //            }

    //            Destroy(currentBattery);
    //            InventoryManager.Instance.AddItem("Battery");

    //            if (batteryImagePrefab != null && inventoryUI != null)
    //            {
    //                GameObject batteryIcon = Instantiate(batteryImagePrefab, inventoryUI);
    //                batteryIcon.name = "Battery";
    //            }

    //            panel.SetActive(false);
    //        }
    //    }
    //}
}
