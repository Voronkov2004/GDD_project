using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public GameObject targetObject; 
    public string requiredPuzzle; 

    void Start()
    {
        if (IsPuzzleSolved())
        {
            ActivateObject();
        }
    }

    public void UpdateActivator()
    {
        if (IsPuzzleSolved())
        {
            ActivateObject();
        }
    }

    private void ActivateObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
            Debug.Log($"Object {targetObject.name} activated.");
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
        else if (requiredPuzzle == "Toilet")
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
        else if (requiredPuzzle == "Basketball")
        {
            return GameStateManager.Instance.isDugUp;
        }
        else if (requiredPuzzle == "LibraryChest")
        {
            return GameStateManager.Instance.isStorage2Solved;
        }
        else if (requiredPuzzle == "BeachChest")
        {
            return GameStateManager.Instance.isBeachChestOpen;
        }
        return true;
    }
}

