using UnityEngine;
using UnityEngine.EventSystems;

public class GridSquare : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped.name == "Player1" || dropped.name == "Player2")
        {
            SetPosition(dropped);
            SetSize(dropped);
        }
    }

    private void SetSize(GameObject dropped)
    {
        var droppedRectTrans = dropped.GetComponent<RectTransform>();
        float heightOffset = Screen.height / 30;
        droppedRectTrans.offsetMin = new Vector2(0, 0);
        droppedRectTrans.offsetMax = new Vector2(0, heightOffset);
    }

    private void SetPosition(GameObject dropped)
    {
        dropped.transform.SetParent(transform);
        dropped.transform.position = transform.position;
    }
}