using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 offset;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Convert mouse position to world point
        Vector3 mouseWorldPoint = cam.ScreenToWorldPoint(eventData.position);
        offset = transform.position - new Vector3(mouseWorldPoint.x, mouseWorldPoint.y, transform.position.z);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update object position based on mouse position
        Vector3 mouseWorldPoint = cam.ScreenToWorldPoint(eventData.position);
        Vector3 newPos = new Vector3(mouseWorldPoint.x, mouseWorldPoint.y, transform.position.z) + offset;
        transform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // You can add code here if needed when dragging ends
    }
}
