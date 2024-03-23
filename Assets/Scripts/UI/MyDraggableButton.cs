using UnityEngine;
using UnityEngine.EventSystems;

public class MyDraggableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Transform _viewImageTransform;
    private bool _isPressed;

    private void Start()
    {
        _isPressed = false;
        _viewImageTransform = transform.GetChild(0);
    }

    private void Update()
    {
        if (_isPressed)
        {
            _viewImageTransform.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        _viewImageTransform.transform.position = transform.position;
    }
}
