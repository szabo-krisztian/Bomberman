using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isPressed;
    private Vector3 _defaultPosition;

    private void Start()
    {
        _isPressed = false;
        _defaultPosition = transform.position;
    }

    private void Update()
    {
        if (_isPressed)
        {
            MoveImageAround();   
        }
    }

    private void MoveImageAround()
    {
        Vector3 cursorPosition = Input.mousePosition;
        transform.position = cursorPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.position = _defaultPosition;
        _isPressed = false;
    }
}
