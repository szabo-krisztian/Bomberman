using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEditorManager : MonoBehaviour
{
    public GameEvent<int> InvalidPlayerPosition;

    [SerializeField]
    private TilemapSO _loadedMap;

    [SerializeField]
    private TileBase _wallTile;

    [SerializeField]
    private TileBase _boxTile;
    
    private Tilemap _obstacleTilemap;
    private Tilemap _backgroundTilemap;
    private BoundsInt _tilemapBounds;
    private TileBase _activeTile;
    private bool _isPlayerBeingPlayed;

    private void Start()
    {
        Tilemap[] tilemaps = GetComponentsInChildren<Tilemap>();
        _obstacleTilemap = tilemaps[0];
        _backgroundTilemap = tilemaps[1];
        _tilemapBounds = GetTilemapBounds();
        _activeTile = _boxTile;
        _isPlayerBeingPlayed = false;
        InitializeTilemap();
        _loadedMap.SetPlayerOutsideMap(1);
        _loadedMap.SetPlayerOutsideMap(2);
    }

    private BoundsInt GetTilemapBounds()
    {
        BoundsInt tilemapBounds = _backgroundTilemap.cellBounds;

        tilemapBounds.xMin += 1;
        tilemapBounds.xMax -= 1;
        tilemapBounds.yMin += 1;
        tilemapBounds.yMax -= 1;

        return tilemapBounds;
    }

    private void InitializeTilemap()
    {
        foreach (TileData tileData in _loadedMap.TilemapData.Tiles)
        {
            Vector3Int tilePosition = tileData.Position;
            if (tileData.TileType == "Brick")
            {
                _obstacleTilemap.SetTile(tilePosition, _boxTile);
            }
            else if (tileData.TileType == "Block")
            {
                _obstacleTilemap.SetTile(tilePosition, _wallTile);
            }
        }
    }

    private void Update()
    {
        Vector3Int cursorInTilemapPosition = GetCursorInTilemapPosition();
        if (Input.GetMouseButton(0) && IsUserAbleToSetTiles(cursorInTilemapPosition))
        {
            _obstacleTilemap.SetTile(cursorInTilemapPosition, _activeTile);
        }
    }

    private bool IsUserAbleToSetTiles(Vector3Int position)
    {
        bool isPositionInsideTilemap = _tilemapBounds.Contains(position);
        bool isPlayerOneNotOnPosition = position != _loadedMap.PlayerTwoPosition;
        bool isPlayerTwoNotOnPosition = position != _loadedMap.PlayerTwoPosition;
        return isPositionInsideTilemap && !_isPlayerBeingPlayed && isPlayerOneNotOnPosition && isPlayerTwoNotOnPosition;
    }

    private Vector3Int GetCursorInTilemapPosition()
    {
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return _backgroundTilemap.WorldToCell(cursorWorldPosition);
    }

    public void PlayerBeingPlacedHandler(Void data)
    {
        _isPlayerBeingPlayed = true;
    }

    public void PlayerHasBeenPlacedHandler(PlayerInfo playerInfo)
    {
        Vector3Int playerInTilemapPosition = _backgroundTilemap.WorldToCell(playerInfo.WorldPosition);

        if (IsPlayerPositionBlocked(playerInfo.PlayerIndex, playerInTilemapPosition))
        {
            OnInvalidPlayerPosition(playerInfo.PlayerIndex);
            _loadedMap.SetPlayerOutsideMap(playerInfo.PlayerIndex);
        }
        else
        {
            UpdatePlayerPosition(playerInfo.PlayerIndex, playerInTilemapPosition);
        }

        _isPlayerBeingPlayed = false;
    }

    private bool IsPlayerPositionBlocked(int playerIndex, Vector3Int position)
    {
        bool arePlayerPositionsMatch = (playerIndex == 1 && position == _loadedMap.PlayerTwoPosition) || (playerIndex == 2 && position == _loadedMap.PlayerOnePosition);
        if (arePlayerPositionsMatch)
        {
            return true;
        }
        return !_tilemapBounds.Contains(position) || _obstacleTilemap.HasTile(position);
    }

    private void UpdatePlayerPosition(int playerIndex, Vector3Int position)
    {
        if (playerIndex == 1)
        {
            _loadedMap.PlayerOnePosition = position;
        }
        else
        {
            _loadedMap.PlayerTwoPosition = position;
        }
    }

    private void OnInvalidPlayerPosition(int playerIndex)
    {
        InvalidPlayerPosition.Raise(playerIndex);
    }

    public void SaveButtonHitHandler(Void data)
    {
        Debug.Log("Save button hit");
    }

    public void DeleteButtonHitHandler(Void data)
    {
        Debug.Log("Delete button hit");
    }

    public void ExitButtonHitHandler(Void data)
    {
        Debug.Log("Exit button hit");
    }
}
