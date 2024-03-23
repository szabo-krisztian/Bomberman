using UnityEngine;
using UnityEngine.EventSystems;

public class MyDraggableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameEvent<Void> PlayerBeingPlaced;
    public GameEvent<PlayerInfo> PlayerHasBeenPlaced;

    private Transform _viewImageTransform;
    private RectTransform _viewImageRectTransform;
    
    private bool _isButtonPressed;
    private int _playerIndex;

    private void Start()
    {
        _viewImageTransform = transform.GetChild(0);
        _viewImageRectTransform = GetComponentInChildren<RectTransform>();
        _isButtonPressed = false;
        _playerIndex = GetPlayerIndex();
    }

    private int GetPlayerIndex()
    {
        return gameObject.name == "Player1" ? 1 : 2;
    }

    private void Update()
    {
        if (_isButtonPressed)
        {
            _viewImageTransform.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isButtonPressed = true;
        _viewImageRectTransform.localScale = new Vector3(0.5f,0.5f, 1f);
        OnPlayerBeingPlaced();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isButtonPressed = false;

        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector3Int tilemapPosition = UtilityFunctions.GetTilemapPosition(mouseWorldPosition);
        Vector3 tilemapCenterPosition = UtilityFunctions.GetCenterPosition(tilemapPosition);

        _viewImageTransform.transform.position = Camera.main.WorldToScreenPoint(tilemapCenterPosition);

        OnPlayerHasBeenPlaced(mouseWorldPosition);
    }

    private void OnPlayerBeingPlaced()
    {
        PlayerBeingPlaced.Raise(new Void());
    }

    private void OnPlayerHasBeenPlaced(Vector3 mousePosition)
    {
        PlayerInfo playerInfo = new PlayerInfo(mousePosition, _playerIndex);
        PlayerHasBeenPlaced.Raise(playerInfo);
    }

    public void InvalidPlayerPositionHandler(int playerIndex)
    {
        if (_playerIndex == playerIndex)
        {
            _viewImageRectTransform.localScale = new Vector3(1f, 1f, 1f);
            _viewImageTransform.transform.position = transform.position;
        }
    }
}
