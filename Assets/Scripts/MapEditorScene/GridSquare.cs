using UnityEngine;
using UnityEngine.EventSystems;

public class GridSquare : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedUIElement = eventData.pointerDrag;
        if (draggedUIElement.name == "Player1" || draggedUIElement.name == "Player2")
        {
            SetPosition(draggedUIElement);
            SetSize(draggedUIElement);
        }
    }

    private void SetSize(GameObject dropped)
    {
        var droppedRectTrans = dropped.GetComponent<RectTransform>();
        float heightOffset = Screen.height / 15 - Screen.height / 20;
        droppedRectTrans.offsetMin = new Vector2(0, Screen.height / 15 / 4);
        droppedRectTrans.offsetMax = new Vector2(0, heightOffset);
    }

    private void SetPosition(GameObject dropped)
    {
        dropped.transform.SetParent(transform);
        dropped.transform.position = transform.position;
    }
}