using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Generic button for our UI elements. We can set it's sprites and custom events.
/// </summary>
/// <typeparam name="T">Type of the data that the buttons sends through event calls.</typeparam>
public abstract class MyButtonBase<T> : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _clickedSprite;
    private TextMeshProUGUI _buttonText;
    private Image _buttonImage;
    private RectTransform _buttonRect;

    public GameEvent<T> ButtonClicked;

    /// <summary>
    /// Built-in Unity method. Queries the components attached to the button GameObject.
    /// </summary>
    private void Start()
    {
        _buttonImage = GetComponent<Image>();
        _buttonRect = GetComponent<RectTransform>();
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// Built-in unity interface. This method calls when the button has been pressed.
    /// </summary>
    /// <param name="eventData">Unity's event parameter.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        _buttonImage.sprite = _clickedSprite;
        Vector2 lowerTextPosition = _buttonText.transform.position + new Vector3(0, -(GetCurrentButtonHeight() / 6));
        MoveButtonText(lowerTextPosition);
    }

    /// <summary>
    /// Built-in unity interface. This method calls when the button has been released.
    /// </summary>
    /// <param name="eventData">Unity's event parameter.</param>
    public void OnPointerUp(PointerEventData eventData)
    {
        _buttonImage.sprite = _defaultSprite;
        Vector2 defaultTextPosition = transform.position + new Vector3(0, (GetCurrentButtonHeight() / 15));
        MoveButtonText(defaultTextPosition);

        OnEventCall();
    }

    /// <summary>
    /// Helper method that aligns the text of our button.
    /// </summary>
    /// <param name="newPosition">Screen position of the new text box.</param>
    private void MoveButtonText(Vector2 newPosition)
    {
        _buttonText.transform.position = newPosition;
    }

    private float GetCurrentButtonHeight()
    {
        return _buttonRect.rect.size.y;
    }

    /// <summary>
    /// Custom virtual method. All buttons can specify their unique behaviour when clicked.
    /// </summary>
    protected abstract void OnEventCall();
}
