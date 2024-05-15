using UnityEngine;
using UnityEngine.EventSystems;

public class GridSquare : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// When the player's drag UI element has been placed this method calls. It changes the size attributes of the player UI element.
    /// </summary>
    /// <param name="eventData">Unity's event parameter.</param>
    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedUIElement = eventData.pointerDrag;
        if (draggedUIElement.name == "Player1" || draggedUIElement.name == "Player2")
        {
            SetPosition(draggedUIElement);
            SetSize(draggedUIElement);
        }
    }

    /// <summary>
    /// Helper method for changing the size of our draggable UI element.
    /// </summary>
    /// <param name="dropped">Unity's event parameter.</param>
    private void SetSize(GameObject dropped)
    {
        var droppedRectTrans = dropped.GetComponent<RectTransform>();
        float heightOffset = Screen.height / 15 - Screen.height / 20;
        droppedRectTrans.offsetMin = new Vector2(0, Screen.height / 15 / 4);
        droppedRectTrans.offsetMax = new Vector2(0, heightOffset);
    }

    /// <summary>
    /// Helper method for changing the position of our draggable UI element.
    /// </summary>
    /// <param name="dropped">Unity's event parameter.</param>
    private void SetPosition(GameObject dropped)
    {
        dropped.transform.SetParent(transform);
        dropped.transform.position = transform.position;
    }
}