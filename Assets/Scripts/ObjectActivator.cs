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
        return true;
    }
}

