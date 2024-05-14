using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameEvent<Void> PlayerBeingPlaced;
    public GameEvent<PlayerInfo> PlayerHasBeenPlaced;

    private Image _image;
    private AspectRatioFitter _aspectRatioFitter;
    private RectTransform _rectTransform;
    private Transform _originalParent;

    private int _playerIndex;
    private bool _isInTilemap;
    private Vector2 _lastScreenSize;

    /// <summary>
    /// Built-in Unity method. We set the basic fields of our DraggableUI element and start a Coroutine that is going to be responsible for the responsivity of the UI.
    /// </summary>
    private void Start()
    {
        _image = GetComponent<Image>();
        _aspectRatioFitter = GetComponent<AspectRatioFitter>();
        _rectTransform = GetComponent<RectTransform>();
        _originalParent = transform.parent;

        _playerIndex = gameObject.name == "Player1" ? 1 : 2;
        _isInTilemap = false;
        _lastScreenSize = GetScreenSize();

        StartCoroutine(ResizeAutomaticallyIfScreenSizeChanged());
    }

    /// <summary>
    /// Coroutine that modifies the size of our UI element.
    /// </summary>
    /// <returns>Coroutine specific type.</returns>
    private IEnumerator ResizeAutomaticallyIfScreenSizeChanged()
    {
        Vector2 currentScreenSize;
        while (true)
        {
            currentScreenSize = GetScreenSize();

            if (currentScreenSize != _lastScreenSize && _isInTilemap)
            {
                float heightOffset = Screen.height / 15 - Screen.height / 20;
                _rectTransform.offsetMin = new Vector2(0, Screen.height / 15 / 4);
                _rectTransform.offsetMax = new Vector2(0, heightOffset);
            }

            _lastScreenSize = currentScreenSize;
            yield return null;
        }
    }

    /// <summary>
    /// Built-in Unity event handler that calls if the UI is being placed.
    /// </summary>
    /// <param name="eventData">Unity's event parameter.</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnPlayerBeingPlaced();

        _aspectRatioFitter.enabled = false;
        _image.raycastTarget = false;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        // 15 represents the map size, 12.5 is also the map size with an offset, so the button's height it bigger than it's width
        _rectTransform.sizeDelta = new Vector2(Screen.height / 15 - Screen.width, Screen.height / 15f - Screen.height);
    }

    /// <summary>
    /// Built-in Unity event handler that calls if the UI is being placed each frame.
    /// </summary>
    /// <param name="eventData">Unity's event parameter.</param>
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    /// <summary>
    /// Built-in Unity event handler that calls if the UI element has been placed.
    /// </summary>
    /// <param name="eventData">Unity's event parameter.</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;

        Vector3 buttonWorldPosition = Camera.main.ScreenToWorldPoint(transform.position);
        OnPlayerHasBeenPlaced(buttonWorldPosition);
    }

    /// <summary>
    /// Helper method raising the event PlayerBeingPlaced.
    /// </summary>
    private void OnPlayerBeingPlaced()
    {
        PlayerBeingPlaced.Raise(new Void());
    }

    /// <summary>
    /// Helper method raising the event PlayerHasBeenPlaced with the corresponding pieces of data.
    /// </summary>
    /// <param name="buttonTilemapPosition">Player's position.</param>
    private void OnPlayerHasBeenPlaced(Vector3 buttonTilemapPosition)
    {
        _isInTilemap = true;
        PlayerInfo playerInfo = new PlayerInfo(buttonTilemapPosition, _playerIndex);
        PlayerHasBeenPlaced.Raise(playerInfo);
    }

    /// <summary>
    /// Event handler method that calls if the user has placed the player on the wrong position.
    /// </summary>
    /// <param name="playerIndex">Index of a player.</param>
    public void InvalidPlayerPositionHandler(int playerIndex)
    {
        if (playerIndex == _playerIndex)
        {
            transform.SetParent(_originalParent);
            _aspectRatioFitter.enabled = true;
            _isInTilemap = false;
        }
    }

    private Vector2 GetScreenSize()
    {
        return new Vector2(Screen.width, Screen.height);
    }
}
