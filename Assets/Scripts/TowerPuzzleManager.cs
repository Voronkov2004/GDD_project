using UnityEngine;

public class TowelPuzzleManager : MonoBehaviour
{
    public DropZone[] dropZones;
    public GameObject successPanel;

    private string[] correctOrder = { "Blue", "Yellow", "Red", "Green" };

    void Start()
    {
        if (successPanel != null)
        {
            successPanel.SetActive(false);
        }
    }

    public void CheckPuzzleCompletion()
    {
        for (int i = 0; i < dropZones.Length; i++)
        {
            if (string.IsNullOrEmpty(dropZones[i].placedTowelColor))
            {
                return;
            }

            if (dropZones[i].placedTowelColor != correctOrder[i])
            {
                Debug.Log("Incorrect towel at position " + (i + 1));
                return;
            }
        }

        // All towels are in the correct order
        Debug.Log("Puzzle Solved!");
        if (successPanel != null)
        {
            successPanel.SetActive(true);
        }

        // Optionally, disable further dragging
        DisableAllDraggables();
    }

    private void DisableAllDraggables()
    {
        Draggable[] draggables = FindObjectsOfType<Draggable>();
        foreach (Draggable draggable in draggables)
        {
            draggable.enabled = false;
        }
    }
}
