using UnityEngine;
using UnityEngine.EventSystems;

public class DragToScale : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 startScale;
    Vector3 mousePos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        startScale = transform.localScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        //transform.position = Input.mousePosition;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log("MousePosition = " + Input.mousePosition);
        Debug.Log("MousePosition in world = " + mousePos);

        //float distance = Vector3.Distance(transform.position, mousePos);
        transform.localScale += mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.localScale = startScale; 
    }
}
