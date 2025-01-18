using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public string itemId; // Unique ID for the item
    public GameObject itemPrefab; // Prefab of the item
    public string requiredPuzzle;

    private GameObject spawnedItem; // Reference to the spawned item

    void Start()
    {
        // Check if the item is already picked up and puzzle solved (if needed)
        if (IsPuzzleSolved() && !GameStateManager.Instance.IsItemPickedUp(itemId))
        {
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        if (itemPrefab != null)
        {
            spawnedItem = Instantiate(itemPrefab, transform.position, transform.rotation);
            spawnedItem.name = itemId;
        }
    }

    public void OnItemPickedUp()
    {
        // Mark the item as picked up
        GameStateManager.Instance.MarkItemPickedUp(itemId);

        // Destroy the item
        if (spawnedItem != null)
        {
            Destroy(spawnedItem);
        }
    }

    private bool IsPuzzleSolved()
    {
        if (requiredPuzzle == "Chest")
        {
            return GameStateManager.Instance.isChestPuzzleSolved;
        }
        else if (requiredPuzzle == "Towel")
        {
            return GameStateManager.Instance.isTowelPuzzleSolved;
        }
        else if(requiredPuzzle == "Toilet")
        {
            return GameStateManager.Instance.isLockerOpened;
        }
        else if (requiredPuzzle == "Cupboard")
        {
            return GameStateManager.Instance.isCupboardUnlocked;
        }
        else if (requiredPuzzle == "Storage")
        {
            return GameStateManager.Instance.isStorageSolved;
        }
        else if (requiredPuzzle == "Machete")
        {
            return GameStateManager.Instance.isNetCut;
        }
        else if (requiredPuzzle == "Floor")
        {
            return GameStateManager.Instance.isFloorOpen;
        }
        return true;
    }

    public void UpdateItemSpawner()
    {
        if (IsPuzzleSolved() && !GameStateManager.Instance.IsItemPickedUp(itemId))
        {
            if (spawnedItem == null) 
            {
                SpawnItem();
            }
        }
    }
}

