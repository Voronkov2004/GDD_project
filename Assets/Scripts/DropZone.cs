using UnityEngine;

public class DropZone : MonoBehaviour
{
    public int positionIndex; // Set this in the Inspector for each drop zone (0, 1, 2, 3)
    public string placedTowelColor; // The color of the towel currently placed here
    public bool isOccupied = false; // To check if the drop zone is already occupied

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure the drop zone is not already occupied
        if (isOccupied)
        {
            return;
        }

        Towel towel = other.GetComponent<Towel>();
        if (towel != null)
        {
            // Snap the towel to the drop zone position
            other.transform.position = transform.position;

            // Record the towel's color
            placedTowelColor = towel.towelColor;
            isOccupied = true;

            // Disable dragging for the placed towel
            Draggable draggable = other.GetComponent<Draggable>();
            if (draggable != null)
            {
                draggable.enabled = false;
            }

            // Check if the puzzle is completed
            FindObjectOfType<TowelPuzzleManager>().CheckPuzzleCompletion();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Towel towel = other.GetComponent<Towel>();
        if (towel != null)
        {
            // Clear the record when the towel leaves the drop zone
            placedTowelColor = null;
            isOccupied = false;

            // Enable dragging again
            Draggable draggable = other.GetComponent<Draggable>();
            if (draggable != null)
            {
                draggable.enabled = true;
            }

            // Update the puzzle state
            FindObjectOfType<TowelPuzzleManager>().CheckPuzzleCompletion();
        }
    }
}
