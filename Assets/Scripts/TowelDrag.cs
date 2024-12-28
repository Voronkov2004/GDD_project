using UnityEngine;

public class TowelDrag : MonoBehaviour
{
    private Vector3 startPosition; // Starting position of the towel
    private bool isDragging = false;
    private bool isOverHook = false; // Tracks if the towel is over a hook
    private GameObject currentHook; // Tracks the current hook under the towel

    public Transform anchorPoint; // Reference to the anchor point on the towel

    void Start()
    {
        // Save the starting position of the towel
        startPosition = transform.position;

        // Find the anchor point
        if (anchorPoint == null)
        {
            anchorPoint = transform.Find("Anchor");
            if (anchorPoint == null)
            {
                Debug.LogError($"Anchor point not found on {name}! Please add an 'Anchor' child object.");
            }
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            // Move the towel with the mouse
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (!isOverHook || currentHook == null)
        {
            // If not over a hook, return to the starting position
            transform.position = startPosition;
            Debug.Log($"{name} was dropped outside of a hook and returned to its starting position.");
        }
        else
        {
            // Snap the anchor point to the hook's position
            Vector3 offset = currentHook.transform.position - anchorPoint.position;
            transform.position += offset;

            // Update starting position to the hook's position
            startPosition = transform.position;

            Debug.Log($"{name} was successfully placed on hook {currentHook.name}.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Only consider hooks, ignore other colliders
        if (other.CompareTag("Hook"))
        {
            isOverHook = true;
            currentHook = other.gameObject;
            Debug.Log($"{name} is over hook {currentHook.name}.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Only handle hooks
        if (other.CompareTag("Hook") && other.gameObject == currentHook)
        {
            isOverHook = false;
            currentHook = null;
            Debug.Log($"{name} is no longer over a hook.");
        }
    }
}