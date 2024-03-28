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

    private IEnumerator ResizeAutomaticallyIfScreenSizeChanged()
    {
        Vector2 currentScreenSize;
        while (true)
        {
            currentScreenSize = GetScreenSize();

            if (currentScreenSize != _lastScreenSize && _isInTilemap)
            {
                float heightOffset = Screen.height / 30;
                _rectTransform.offsetMin = new Vector2(0, 0);
                _rectTransform.offsetMax = new Vector2(0, heightOffset);
            }

            _lastScreenSize = currentScreenSize;
            yield return null;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnPlayerBeingPlaced();

        _aspectRatioFitter.enabled = false;
        _image.raycastTarget = false;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        _rectTransform.sizeDelta = new Vector2(Screen.height / 15 - Screen.width, Screen.height / 10 - Screen.height);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;

        Vector3 buttonWorldPosition = Camera.main.ScreenToWorldPoint(transform.position);
        OnPlayerHasBeenPlaced(buttonWorldPosition);
    }

    private void OnPlayerBeingPlaced()
    {
        PlayerBeingPlaced.Raise(new Void());
    }

    private void OnPlayerHasBeenPlaced(Vector3 buttonTilemapPosition)
    {
        _isInTilemap = true;
        PlayerInfo playerInfo = new PlayerInfo(buttonTilemapPosition, _playerIndex);
        PlayerHasBeenPlaced.Raise(playerInfo);
    }

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
