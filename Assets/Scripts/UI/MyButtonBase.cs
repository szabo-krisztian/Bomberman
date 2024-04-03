using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class MyButtonBase<T> : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _clickedSprite;
    private TextMeshProUGUI _buttonText;
    private Image _buttonImage;
    private RectTransform _buttonRect;

    public GameEvent<T> ButtonClicked;

    private void Start()
    {
        _buttonImage = GetComponent<Image>();
        _buttonRect = GetComponent<RectTransform>();
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _buttonImage.sprite = _clickedSprite;
        Vector2 lowerTextPosition = _buttonText.transform.position + new Vector3(0, -(GetCurrentButtonHeight() / 6));
        MoveButtonText(lowerTextPosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _buttonImage.sprite = _defaultSprite;
        Vector2 defaultTextPosition = transform.position + new Vector3(0, (GetCurrentButtonHeight() / 15));
        MoveButtonText(defaultTextPosition);

        OnEventCall();
    }

    private void MoveButtonText(Vector2 newPosition)
    {
        _buttonText.transform.position = newPosition;
    }

    private float GetCurrentButtonHeight()
    {
        return _buttonRect.rect.size.y;
    }

    protected abstract void OnEventCall();
}
