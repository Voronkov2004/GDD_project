using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    //Audio
    public AudioSource audioSource;
    public AudioSource interactionAudioSource; //doors 
    public AudioClip lockerOpenSound; //wc lockers openings
    public AudioClip openDoorSound;
    public AudioClip keyOpeningCupboard; //using key to open cabinets
    public AudioClip chainsSound; //chains
    public AudioClip knifeCutting; //machete in the end
    public AudioClip closedDoorSound;
    public AudioClip boltSound;
    public AudioClip shovelSound;
    public AudioClip breakingWood;
    public AudioClip itemPickupSound; //keys pick up sound
    public AudioClip notePickupSound; //notes, diary, poster pick up sound
    public AudioClip batteryPickupSound; // batteries' pick up sound
    public AudioClip flashlightPickupSound; // flashlight pick up sound
    public AudioClip metalItemPickupSound; // crowbar, bolt cutter, shovel
    public AudioClip medallionPickupSound; // medallion pickup sound

    // UI and Inventory
    public GameObject panel;
    public TextMeshProUGUI panelText;
    public GameObject keyImagePrefab;
    public GameObject keyKitchenLockerImagePrefab;
    public GameObject flashlightImagePrefab;
    public GameObject batteryImagePrefab;
    public GameObject shovelImagePrefab;
    public TextMeshProUGUI lockerMessage;
    public GameObject combinedFlashlightImagePrefab;
    public GameObject boltCutterImagePrefab;
    public GameObject theatreKeyImagePrefab;
    public GameObject MacheteImagePrefab;
    public GameObject KeysGatesToPondImagePrefab;
    public GameObject CrowBarImage;
    public Transform inventoryUI;
    public GameObject notePanel;
    public TextMeshProUGUI noteTextUI;
    public GameObject lockerPicture;
    public GameObject lockerNote;
    public GameObject lockerTrigger;
    public GameObject closedLockerPanel;
    public GameObject beachOpenChest;
    public GameObject beachClosedChest;
    public GameObject openLockerPanel;
    public GameObject dirtPile;
    public GameObject GoldKeyImage;
    public GameObject openedLockerInScene;
    public GameObject openedLocker;
    public GameObject openedFloor;
    public GameObject openedGates;
    public GameObject closedGates;
    public GameObject mirrorCanvas; // Canvas displaying the mirror UI
    public Image mirrorImage; // Image for the zoomed-in mirror
    public Image steamImage; // Image for the steam effect
    public Image dimBackground; // Dimmed background for the mirror
    public float steamAnimationDuration = 2f; // Duration of the steam animation
    public GameObject openChestImage;

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
    private bool isFinalNote = false;
    private bool isLockerInteractionActive = false;
    private bool isClosedLockerSceneOpen = false;
    private bool isInsideChestTrigger = false;
    private bool isInsideTowelsGameTrigger = false;
    private bool isInsideKeysGatesToPondTrigger = false;
    private bool isTheaterTrigger = false;
    private bool isStorageTrigger = false;
    private bool isBackToTheaterTrigger = false;
    private bool isLibraryTrigger = false;
    private bool isInsideNetTrigger = false;
    private bool isInsideTentTrigger = false;
    private bool isMacheteTrigger = false;
    private bool isMapTrigger = false;
    private bool isShovelTrigger = false;
    private bool isRealKitchenTrigger = false;
    private bool isInsideTheatreKeyTrigger = false;
    private bool isInsideCupboardTrigger = false;
    private bool isNearMirror = false;
    private bool hasSeenMirrorSteam = false;
    private bool isInsideChestWithCodeTrigger = false;
    private bool isInsideCrowbarTrigger = false;
    private bool isInsidePickMeTrigger = false;
    private bool isInsideGateTrigger = false;
    private bool isInsideOpenGateTrigger = false;
    private bool isInsideCourtTrigger = false;
    private bool isInsideClosedCaseTrigger = false;
    private bool isInsideGoldKeyTrigger = false;
    private bool isInsideBeachChestTrigger = false;
    // Current Objects
    private GameObject currentKey;
    private GameObject currentFlashlight;
    private GameObject currentBattery;
    private GameObject currentMachete;
    private GameObject currentNote;
    private GameObject currentBoltCutter;
    private GameObject currentTheatreKey;
    private GameObject currentKeysGatesToPond;
    private GameObject currentCrowbar;
    private GameObject currentShovel;
    private GameObject currentGoldKey;

    void Start()
    {
        if (GameStateManager.Instance.isStorageSolved)
        {
            GameObject chestWithCode = GameObject.FindWithTag("ChestWithCode");
            if (chestWithCode != null)
            {
                chestWithCode.SetActive(false);
                Debug.Log("ChestWithCode trigger has been disabled because the storage puzzle is solved.");
            }
            else
            {
                Debug.LogWarning("Object with tag 'ChestWithCode' not found in the scene.");
            }
        }

        if (openedLocker != null)
        {
            openedLocker.SetActive(false);
        }
        if (openedFloor != null)
        {
            openedFloor.SetActive(GameStateManager.Instance.isFloorOpen);
        }
        if (openedGates != null)
        {
            openedGates.SetActive(GameStateManager.Instance.isGatesOpen);
        }
        if (closedGates != null)
        {
            closedGates.SetActive(!GameStateManager.Instance.isGatesOpen);
        }
        if (beachOpenChest != null)
        {
            beachOpenChest.SetActive(GameStateManager.Instance.isBeachChestOpen);
        }
        if (beachClosedChest != null)
        {
            beachClosedChest.SetActive(!GameStateManager.Instance.isBeachChestOpen);
        }
        if (dirtPile != null)
        {
            dirtPile.SetActive(GameStateManager.Instance.isDugUp);
        }
        if (mirrorCanvas != null)
            mirrorCanvas.SetActive(false);

        if (steamImage != null)
            steamImage.gameObject.SetActive(false);

        if (dimBackground != null)
            dimBackground.gameObject.SetActive(false);            

        // Check if the cupboard is already unlocked and make the openedLocker visible if it is
        if (GameStateManager.Instance.isCupboardUnlocked && openedLocker != null)
        {
            openedLocker.SetActive(true);
        }
        if (!GameStateManager.Instance.isCupboardUnlocked && openedLocker != null)
        {
            openedFloor.SetActive(true);
        }
        if (GameStateManager.Instance.isDugUp && dirtPile != null)
        {
            dirtPile.SetActive(true);
        }
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

        if (GameStateManager.Instance.isStorageSolved && openChestImage != null)
        {
            openChestImage.SetActive(true);
            foreach (ItemSpawner spawner in FindObjectsOfType<ItemSpawner>())
                    {
                        spawner.UpdateItemSpawner();
                    }

                    foreach (ObjectActivator activator in FindObjectsOfType<ObjectActivator>())
                    {
                        activator.UpdateActivator();
                    }
        }
        if (GameStateManager.Instance.isStorage2Solved && openChestImage != null)
        {
            openChestImage.SetActive(true);
            foreach (ItemSpawner spawner in FindObjectsOfType<ItemSpawner>())
                    {
                        spawner.UpdateItemSpawner();
                    }

                    foreach (ObjectActivator activator in FindObjectsOfType<ObjectActivator>())
                    {
                        activator.UpdateActivator();
                    }
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
                else if (tag == "KeysGatesToPond" && KeysGatesToPondImagePrefab != null && inventoryUI != null)
                {
                    GameObject KeysGatesToPondIcon = Instantiate(KeysGatesToPondImagePrefab, inventoryUI);
                    KeysGatesToPondIcon.name = "KeysGatesToPond";
                }
                else if (tag == "GoldKey" && GoldKeyImage != null && inventoryUI != null)
                {
                    GameObject GoldKey = Instantiate(GoldKeyImage, inventoryUI);
                    GoldKey.name = "GoldKey";
                }
                else if (tag == "BoltCutter" && boltCutterImagePrefab != null && inventoryUI != null)
                {
                    GameObject boltCutterIcon = Instantiate(boltCutterImagePrefab, inventoryUI);
                    boltCutterIcon.name = "BoltCutter";
                }
                else if (tag == "Key_theatre_library" && theatreKeyImagePrefab != null && inventoryUI != null)
                {
                    GameObject theatreKey = Instantiate(theatreKeyImagePrefab, inventoryUI);
                    theatreKey.name = "Key_theatre_library";
                }
                else if (tag == "Machete" && MacheteImagePrefab != null && inventoryUI != null)
                {
                    GameObject machete = Instantiate(MacheteImagePrefab, inventoryUI);
                    machete.name = "Machete";
                }
                else if (tag == "Crowbar" && CrowBarImage != null && inventoryUI != null)
                {
                    GameObject crowBar = Instantiate(CrowBarImage, inventoryUI);
                    crowBar.name = "Crowbar";
                }
                else if (tag == "Shovel" && shovelImagePrefab != null && inventoryUI != null)
                {
                    GameObject shovel = Instantiate(shovelImagePrefab, inventoryUI);
                    shovel.name = "Shovel";
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

        if (isInsideNetTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (InventoryManager.Instance.HasItem("Machete"))
            {
                StartCoroutine(HandleCuttingNet());
            }
            else
            {
                panelText.text = "It seems the net got caught on something, I can't get it out. I need something sharp to cut it.";
                panel?.SetActive(true);
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
            if (audioSource != null && itemPickupSound != null)
            {
                audioSource.PlayOneShot(itemPickupSound);
            }
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
        else if (isInsideKeysGatesToPondTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (audioSource != null && itemPickupSound != null)
            {
                audioSource.PlayOneShot(itemPickupSound);
            }
            ProcessItemPickup(currentKeysGatesToPond, "KeysGatesToPond", KeysGatesToPondImagePrefab);
        }
        else if (isInsideGoldKeyTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (audioSource != null && itemPickupSound != null)
            {
                audioSource.PlayOneShot(itemPickupSound);
            }
            ProcessItemPickup(currentGoldKey, "GoldKey", GoldKeyImage);
        }
        else if (isInsideBatteryTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (audioSource != null && batteryPickupSound != null)
            {
                audioSource.PlayOneShot(batteryPickupSound);
            }
            ProcessItemPickup(currentBattery, "Battery", batteryImagePrefab);
        }
        else if (isMacheteTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (audioSource != null && metalItemPickupSound != null)
            {
                audioSource.PlayOneShot(metalItemPickupSound);
            }
            ProcessItemPickup(currentMachete, "Machete", MacheteImagePrefab);
        }
        else if (isInsideBoltCutterTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (audioSource != null && metalItemPickupSound != null)
            {
                audioSource.PlayOneShot(metalItemPickupSound);
            }
            ProcessItemPickup(currentBoltCutter, "BoltCutter", boltCutterImagePrefab);
        }
        else if (isInsideBoltCutterTrigger && Input.GetKeyDown(KeyCode.F))
        {
            if (audioSource != null && metalItemPickupSound != null)
            {
                audioSource.PlayOneShot(metalItemPickupSound);
            }
            ProcessItemPickup(currentBoltCutter, "BoltCutter", boltCutterImagePrefab);
        }
        else if (isInsideTheatreKeyTrigger && Input.GetKeyDown(KeyCode.F))
        {
            audioSource.PlayOneShot(itemPickupSound);
            ProcessItemPickup(currentTheatreKey, "Key_theatre_library", theatreKeyImagePrefab);
        }
        else if (isInsideCrowbarTrigger && Input.GetKeyDown(KeyCode.F))
        {
            audioSource.PlayOneShot(metalItemPickupSound);
            ProcessItemPickup(currentCrowbar, "Crowbar", CrowBarImage);
        }
        else if (isShovelTrigger && Input.GetKeyDown(KeyCode.F))
        {
            audioSource.PlayOneShot(metalItemPickupSound);
            ProcessItemPickup(currentShovel, "Shovel", shovelImagePrefab);
        }
        // medallion pick up sound dopisat kogda on budet gotov v igre
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

    private IEnumerator DelayedSceneLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadSceneWithSavedPosition(sceneName);
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
                    if (interactionAudioSource != null && openDoorSound != null)
                    {
                        interactionAudioSource.PlayOneShot(openDoorSound);
                        StartCoroutine(DelayedSceneLoad(kitchenSceneName, openDoorSound.length));
                    }
                }
                else if (InventoryManager.Instance.HasItem("Key"))
                {
                    InventoryManager.Instance.RemoveItem("Key");
                    interactionAudioSource.PlayOneShot(openDoorSound);
                    StartCoroutine(DelayedSceneLoad(kitchenSceneName, openDoorSound.length));

                    // Remove one key icon from the inventory UI
                    RemoveItemIconFromUI("Key");

                    GameStateManager.Instance.isKitchenUnlocked = true; //StateManager.kitchenUnlocked = true;
                }
                else
                {
                    if (audioSource != null && closedDoorSound != null)
                    {
                        audioSource.PlayOneShot(closedDoorSound);
                    }
                    panelText.text = "You need a key to unlock this door!";
                    panel.SetActive(true);
                }
            }
            else if (isInsideLadderTrigger)
            {
                SaveCurrentPlayerPosition();
                SceneManager.LoadScene(upstairsSceneName);
            }
            else if (isInsideGateTrigger)
            {
                if (InventoryManager.Instance.HasItem("KeysGatesToPond"))
                {
                    InventoryManager.Instance.RemoveItem("KeysGatesToPond");
                    RemoveItemIconFromUI("KeysGatesToPond");
                    GameStateManager.Instance.isGatesOpen = true;
                    GameStateManager.Instance.SaveProgress();
                    closedGates.SetActive(false);
                    openedGates.SetActive(true);
                }
                else
                {
                    panelText.text = "The gates are locked. I need to find the keys.";
                    panel.SetActive(true);
                }

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
                if (interactionAudioSource != null && openDoorSound != null)
                {
                    interactionAudioSource.PlayOneShot(openDoorSound);
                    StartCoroutine(DelayedSceneLoad("GameScene", openDoorSound.length));
                }
            }
            else if (isBackToTheaterTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition(theaterSceneName);
            }
            else if (isInsideOpenGateTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition("Beach");
            }
            else if (isRealKitchenTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition("RealKitchen");
            }
            else if (isTheaterTrigger)
            {
                SaveCurrentPlayerPosition();
                if (GameStateManager.Instance.isTheaterOpen)
                {
                    interactionAudioSource.PlayOneShot(openDoorSound);
                    StartCoroutine(DelayedSceneLoad(theaterSceneName, openDoorSound.length));
                }
                else if (InventoryManager.Instance.HasItem("Key_theatre_library"))
                {
                    GameStateManager.Instance.isTentLOpened = true;
                    GameStateManager.Instance.SaveProgress();
                    if (interactionAudioSource != null && openDoorSound != null)
                    {
                        interactionAudioSource.PlayOneShot(openDoorSound);
                        StartCoroutine(DelayedSceneLoad(theaterSceneName, openDoorSound.length));
                    }
                }
                else
                {
                    if (audioSource != null && closedDoorSound != null)
                    {
                        audioSource.PlayOneShot(closedDoorSound);
                    }
                    panelText.text = "You need a key to unlock the theater!";
                    panel.SetActive(true);
                }
            }
            else if (isInsideBoardTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition(boardSceneName); // Transition to "Board" scene
            }
            else if (isInsidePickMeTrigger)
            {
                if (InventoryManager.Instance.HasItem("Crowbar"))
                {
                    panelText.text = "Press F to break the floor.";
                    panel.SetActive(true);
                    if (audioSource != null && metalItemPickupSound != null)
                    {
                        audioSource.PlayOneShot(breakingWood);
                    }
                    InventoryManager.Instance.RemoveItem("Crowbar");
                    RemoveItemIconFromUI("Crowbar");
                    openedFloor.SetActive(true);
                    GameStateManager.Instance.isFloorOpen = true;
                    GameStateManager.Instance.SaveProgress();

                    foreach (ItemSpawner spawner in FindObjectsOfType<ItemSpawner>())
                    {
                        spawner.UpdateItemSpawner();
                    }

                    foreach (ObjectActivator activator in FindObjectsOfType<ObjectActivator>())
                    {
                        activator.UpdateActivator();
                    }
                }
                else
                {
                    panelText.text = "I can't pry this board up with my hands. I need some kind of tool.";
                    panel.SetActive(true);
                }
            }
            else if (isInsideToiletTrigger)
            {
                SaveCurrentPlayerPosition();
                if (interactionAudioSource != null && openDoorSound != null)
                {
                    interactionAudioSource.PlayOneShot(openDoorSound);
                    StartCoroutine(DelayedSceneLoad(toiletSceneName, openDoorSound.length));
                }
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
            else if (isNearMirror)
            {
                if (mirrorCanvas != null && mirrorCanvas.activeSelf)
                {
                    CloseMirrorView();
                }
                else
                {
                    ShowMirrorView();
                }
            }
            else if (isLibraryTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition(librarySceneName);
            }
            else if (isInsideClosedCaseTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition("ClosedCupboardScene2");
            }
            else if (isInsideBeachChestTrigger)
            {
                if (InventoryManager.Instance.HasItem("GoldKey"))
                {
                    InventoryManager.Instance.RemoveItem("GoldKey");
                    RemoveItemIconFromUI("GoldKey");
                    GameStateManager.Instance.isBeachChestOpen = true;
                    GameStateManager.Instance.SaveProgress();
                    beachOpenChest.SetActive(true);
                    beachClosedChest.SetActive(false);
                    panel.SetActive(false);

                    foreach (ItemSpawner spawner in FindObjectsOfType<ItemSpawner>())
                    {
                        spawner.UpdateItemSpawner();
                    }

                    foreach (ObjectActivator activator in FindObjectsOfType<ObjectActivator>())
                    {
                        activator.UpdateActivator();
                    }
                }
                else
                {
                    panelText.text = "I need a key to open this chest.";
                    panel.SetActive(true);
                }
            }
            else if (isInsideChestWithCodeTrigger)
            {
                SaveCurrentPlayerPosition();
                LoadSceneWithSavedPosition("ClosedCupboardScene");
            }
            else if (isInsideCourtTrigger)
            {
                if (InventoryManager.Instance.HasItem("Shovel"))
                {
                    InventoryManager.Instance.RemoveItem("Shovel");
                    RemoveItemIconFromUI("Shovel");
                    dirtPile.SetActive(true);
                    GameStateManager.Instance.isDugUp = true;
                    GameStateManager.Instance.SaveProgress();
                    panel.SetActive(false);

                    foreach (ItemSpawner spawner in FindObjectsOfType<ItemSpawner>())
                    {
                        spawner.UpdateItemSpawner();
                    }

                    foreach (ObjectActivator activator in FindObjectsOfType<ObjectActivator>())
                    {
                        activator.UpdateActivator();
                    }
                }
                else
                {
                    panelText.text = "I need a shovel to dig here.";
                    panel.SetActive(true);
                }
            }
            else if (isInsideCupboardTrigger)
            {
                if (GameStateManager.Instance.isCupboardUnlocked)
                {
                    panelText.text = "Cupboard is already unlocked.";
                    panel.SetActive(true);
                }
                else if (InventoryManager.Instance.HasItem("KitchenLockerPrefab"))
                {
                    InventoryManager.Instance.RemoveItem("KitchenLockerPrefab");
                    RemoveItemIconFromUI("KitchenLockerPrefab");
                    GameStateManager.Instance.isCupboardUnlocked = true;
                    GameStateManager.Instance.SaveProgress();
                    SaveCurrentPlayerPosition();
                    // Эта функция открывает шкаф на кухне, болторез добавляй после неё
                    //interactionAudioSource.PlayOneShot(chainsSound);
                    UnlockCupboard();
                    LoadSceneWithSavedPosition("CupboardClosed");
                }
                else
                {
                    panelText.text = "You need a key to unlock the cupboard!";
                    panel.SetActive(true);
                }
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

    //private void UnlockCupboard()
    //{
    //    GameObject cupboard = GameObject.FindWithTag("Cupboard");
    //    if (cupboard != null)
    //    {
    //        cupboard.SetActive(true);
    //        Debug.Log("Cupboard unlocked and made visible.");
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Cupboard not found!");
    //    }
    //}

    private void UnlockCupboard()
    {
        StartCoroutine(UnlockCupboardCoroutine());
    }
    private IEnumerator UnlockCupboardCoroutine()
    {
        GameObject cupboard = GameObject.FindWithTag("Cupboard");
        if (cupboard != null)
        {
            interactionAudioSource.PlayOneShot(chainsSound); 
            yield return new WaitForSecondsRealtime(2f); // Пауза перед открытием
            cupboard.SetActive(true); 
            Debug.Log("Cupboard unlocked and made visible.");
        }
        else
        {
            Debug.LogWarning("Cupboard not found!");
        }
    }

    //private IEnumerator UnlockCupboardCoroutine(GameObject cupboard)
    //{
    //    if (interactionAudioSource != null && chainsSound != null)
    //    {
    //        interactionAudioSource.PlayOneShot(chainsSound);
    //    }

    //    yield return new WaitForSecondsRealtime(2f);

    //    cupboard.SetActive(true);
    //    Debug.Log("Cupboard unlocked and made visible.");
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
        else if (collision.CompareTag("KeysGatesToPond"))
        {
            isInsideKeysGatesToPondTrigger = true;
            currentKeysGatesToPond = collision.gameObject;
            panelText.text = "Press F to pick up the keys for the gates to the pond!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Map"))
        {
            isMapTrigger = true;
            panelText.text = "Based on this drawn map, there seems to be something on the basketball court. I need to go there and check it out.";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("GoldKey"))
        {
            isInsideGoldKeyTrigger = true;
            currentGoldKey = collision.gameObject;
            panelText.text = "Press F to pick up the gold key!";
            panel?.SetActive(true);
        }
        if (collision.CompareTag("Cupboard"))
        {
            isInsideCupboardTrigger = true;
            panelText.text = "Press F to unlock the cupboard!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Battery"))
        {
            isInsideBatteryTrigger = true;
            currentBattery = collision.gameObject;
            panelText.text = "Press F to pick up the battery!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Shovel"))
        {
            isShovelTrigger = true;
            currentShovel = collision.gameObject;
            panelText.text = "Press F to pick up the shovel!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Crowbar"))
        {
            isInsideCrowbarTrigger = true;
            currentCrowbar = collision.gameObject;
            panelText.text = "Press F to pick up the crowbar!";
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
        else if (collision.CompareTag("Machete"))
        {
            isMacheteTrigger = true;
            currentMachete = collision.gameObject;
            panelText.text = "Press F to pick up the machete!";
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
        else if (collision.CompareTag("PickMe") && !GameStateManager.Instance.isFloorOpen)
        {
            isInsidePickMeTrigger = true;
        }
        else if (collision.CompareTag("Gates"))
        {
            isInsideGateTrigger = true;
        }
        else if (collision.CompareTag("FuckingChest"))
        {
            isInsideBeachChestTrigger = true;
            panelText.text = "Press F to see chest!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("ClosedCrate"))
        {
            isInsideClosedCaseTrigger = true;
            panelText.text = "Press F to open the chest!";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("OpenGates"))
        {
            panelText.text = "Press F to open the gates!";
            panel?.SetActive(true);
            isInsideOpenGateTrigger = true;
        }
        else if (collision.CompareTag("Court") && !GameStateManager.Instance.isDugUp)
        {
            panelText.text = "Press F to Dig!";
            interactionAudioSource.PlayOneShot(shovelSound);
            panel?.SetActive(true);
            isInsideCourtTrigger = true;
        }
        else if (collision.CompareTag("Net"))
        {
            isInsideNetTrigger = true;

            if (InventoryManager.Instance.HasItem("Machete"))
            {
                panelText.text = "Press F to cut the net with the machete.";
            }
            else
            {
                panelText.text = "It seems the net got caught on something, I can't get it out. I need something sharp to cut it.";
            }
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
        else if (collision.CompareTag("Key_theatre_library"))
        {
            isInsideTheatreKeyTrigger = true;
            currentTheatreKey = collision.gameObject;
            panelText.text = "Press F to pick up the theatre key.";
            panel?.SetActive(true);
        }
        else if (collision.CompareTag("Mirror"))
        {
            isNearMirror = true;
            if (panel != null && panelText != null)
            {
                panelText.text = "Press F to inspect the mirror.";
                panel.SetActive(true);
            }
        }
        else if (collision.CompareTag("ChestWithCode"))
        {
            isInsideChestWithCodeTrigger = true;
            panelText.text = "Press F to inspect the cupboard.";
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
        else if (collision.CompareTag("KeysGatesToPond"))
        {
            isInsideKeysGatesToPondTrigger = false;
            currentKeysGatesToPond = null;
        }
        else if (collision.CompareTag("Battery"))
        {
            isInsideBatteryTrigger = false;
            currentBattery = null;
        }
        else if (collision.CompareTag("Cupboard"))
        {
            isInsideCupboardTrigger = false;
            panel?.SetActive(false);
        }
        else if (collision.CompareTag("PickMe"))
        {
            isInsidePickMeTrigger = false;
        }
        else if (collision.CompareTag("FuckingChest"))
        {
            isInsideBeachChestTrigger = false;
        }
        else if (collision.CompareTag("ClosedCrate"))
        {
            isInsideClosedCaseTrigger = false;
        }
        else if (collision.CompareTag("Map"))
        {
            isMapTrigger = false;
            panel?.SetActive(false);
        }
        else if (collision.CompareTag("Kitchen"))
        {
            isInsideKitchenTrigger = false;
        }
        else if (collision.CompareTag("Court"))
        {
            isInsideCourtTrigger = false;
        }
        else if (collision.CompareTag("OpenGates"))
        {
            isInsideOpenGateTrigger = false;
        }
        else if (collision.CompareTag("Library"))
        {
            isLibraryTrigger = false;
        }
        else if (collision.CompareTag("Ladder"))
        {
            isInsideLadderTrigger = false;
        }
        else if (collision.CompareTag("Gates"))
        {
            isInsideGateTrigger = false;
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
            isBackToTheaterTrigger = false;
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
        else if (collision.CompareTag("Net"))
        {
            isInsideNetTrigger = false;
            panel?.SetActive(false);
        }
        else if (collision.CompareTag("Machete"))
        {
            isMacheteTrigger = false;
            currentMachete = null;
            panel?.SetActive(false);
        }
        else if (collision.CompareTag("Shovel"))
        {
            isShovelTrigger = false;
            currentShovel = null;
            panel?.SetActive(false);
        }
        else if (collision.CompareTag("Crowbar"))
        {
            isInsideCrowbarTrigger = false;
            currentCrowbar = null;
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
        else if (collision.CompareTag("Key_theatre_library"))
        {
            isInsideTheatreKeyTrigger = false;
            currentTheatreKey = null;
            panel?.SetActive(false);
        }
        else if (collision.CompareTag("Mirror"))
        {
            isNearMirror = false;
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }
        else if (collision.CompareTag("ChestWithCode"))
        {
            isInsideChestWithCodeTrigger = false;
            panel?.SetActive(false);
        }
        else if (collision.CompareTag("GoldKey"))
        {
            isInsideGoldKeyTrigger = false;
            currentGoldKey = null;
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

                if (isFinalNote)
                {
                    SceneManager.LoadScene("TheEnd");
                }

                // Show message if defined in the Note
                if (currentNote != null && currentNote.CompareTag("Note"))
                {
                    Note noteComponent = currentNote.GetComponent<Note>();
                    if (noteComponent != null && !string.IsNullOrEmpty(noteComponent.closeNoteMessage))
                    {
                        StartCoroutine(ShowMessageSequence(noteComponent.closeNoteMessage, noteComponent.followUpMessage, 7f));
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

                    isFinalNote = noteComponent.isFinalNote;

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
            interactionAudioSource.PlayOneShot(lockerOpenSound);
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
        interactionAudioSource.PlayOneShot(boltSound);
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

        GameStateManager.Instance.SaveProgress();

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

    private IEnumerator HandleCuttingNet()
    {
        panel?.SetActive(false);

        GameStateManager.Instance.isNetCut = true;
        GameStateManager.Instance.SaveProgress();

        interactionAudioSource.PlayOneShot(knifeCutting);

        yield return new WaitForSeconds(2f);

        foreach (ObjectActivator activator in FindObjectsOfType<ObjectActivator>())
        {
            activator.UpdateActivator();
        }

        panelText.text = "You found a note.";
        panel?.SetActive(true);

        if (currentNote != null) yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));

        panel?.SetActive(false);

        Debug.LogWarning("Now we show the end scene!");
        LoadSceneWithSavedPosition("EndingScene");
    }


    private void OpenClosedLockerUI()
    {
        if (closedLockerPanel != null)
        {
            closedLockerPanel.SetActive(true);
            DisablePlayerMovement();
            isClosedLockerSceneOpen = true;
            if (!InventoryManager.Instance.HasItem("BoltCutter"))
            {
                //lockerMessage.SetActive(true);
            }
        }
    }

    private void CloseClosedLockerUI()
    {
        if (closedLockerPanel != null)
        {
            closedLockerPanel.SetActive(false);
            EnablePlayerMovement();
            isClosedLockerSceneOpen = false;
            //lockerMessage.SetActive(false);
        }
    }

    private void ShowMirrorView()
    {
        if (mirrorCanvas != null)
            mirrorCanvas.SetActive(true);

        if (dimBackground != null)
            dimBackground.gameObject.SetActive(true);

        if (mirrorImage != null)
            mirrorImage.gameObject.SetActive(true);

        if (hasSeenMirrorSteam || GameStateManager.Instance.hasSeenMirrorSteam)
        {
            // If the steam effect was already shown, display it immediately
            hasSeenMirrorSteam = true;
            if (steamImage != null)
                steamImage.gameObject.SetActive(true);
        }
        else
        {
            // If the steam effect has not been shown, start the animation
            StartCoroutine(AnimateSteamEffect());
        }

        // Disable player movement during the interaction
        DisablePlayerMovement();
    }

    private IEnumerator AnimateSteamEffect()
    {
        if (steamImage != null)
        {
            steamImage.gameObject.SetActive(true);

            // Animate the steam effect from bottom to top
            RectTransform steamRect = steamImage.GetComponent<RectTransform>();
            Vector2 originalSize = steamRect.sizeDelta;
            steamRect.sizeDelta = new Vector2(originalSize.x, 0);

            float elapsedTime = 0f;

            while (elapsedTime < steamAnimationDuration)
            {
                elapsedTime += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsedTime / steamAnimationDuration);
                steamRect.sizeDelta = new Vector2(originalSize.x, originalSize.y * progress);
                yield return null;
            }

            steamRect.sizeDelta = originalSize;

            // Mark the steam effect as shown
            hasSeenMirrorSteam = true;

            // Optionally save the state if needed
            GameStateManager.Instance.hasSeenMirrorSteam = true;
        }
    }

    public void CloseMirrorView()
    {
        // Close the mirror view
        if (mirrorCanvas != null)
            mirrorCanvas.SetActive(false);

        if (dimBackground != null)
            dimBackground.gameObject.SetActive(false);

        if (mirrorImage != null)
            mirrorImage.gameObject.SetActive(false);

        if (steamImage != null)
            steamImage.gameObject.SetActive(false);

        EnablePlayerMovement();

        //if (!hasSeenMirrorSteam && steamImage != null)
        //    steamImage.gameObject.SetActive(false);
    }

}
