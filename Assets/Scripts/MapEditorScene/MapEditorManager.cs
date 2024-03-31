using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private bool _isPlayerBeingPlaced;
    private bool _isMenuPanelOpen;

    private Vector3Int _playerOnePosition;
    private Vector3Int _playerTwoPosition;

    private Dictionary<string, int> _zombies;

    private void Start()
    {
        Tilemap[] tilemaps = GetComponentsInChildren<Tilemap>();
        _obstacleTilemap = tilemaps[0];
        _backgroundTilemap = tilemaps[1];

        _tilemapBounds = GetTilemapBounds();
        _activeTile = _boxTile;
        _isPlayerBeingPlaced = false;
        _isMenuPanelOpen = false;

        SetPlayerOutsideMap(1);
        SetPlayerOutsideMap(2);
        InitZombies();

        InitializeTilemap();
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

    private void SetPlayerOutsideMap(int playerIndex)
    {
        if (playerIndex == 1)
        {
            _playerOnePosition = new Vector3Int(-69, -69, -69);
        }
        else
        {
            _playerTwoPosition = new Vector3Int(-69, -69, -69);
        }
    }

    private void InitZombies()
    {
        _zombies = new Dictionary<string, int>();
        _zombies["Normal"] = 0;
        _zombies["Ghost"] = 0;
        _zombies["Intelligent"] = 0;
        _zombies["VeryIntelligent"] = 0;
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
        if (Input.GetMouseButton(0) && IsUserAbleToPlaceTiles(GetCursorInTilemapPosition()))
        {
            _obstacleTilemap.SetTile(GetCursorInTilemapPosition(), _activeTile);
        }
        if (Input.GetMouseButton(1) && IsUserAbleToPlaceTiles(GetCursorInTilemapPosition()))
        {
            _obstacleTilemap.SetTile(GetCursorInTilemapPosition(), null);
        }
    }

    private bool IsUserAbleToPlaceTiles(Vector3Int position)
    {
        return !_isPlayerBeingPlaced && !_isMenuPanelOpen && _tilemapBounds.Contains(position) && !ArePlayersOnPosition(position) && !IsInPlayersOffset(Input.mousePosition);
    }

    private bool ArePlayersOnPosition(Vector3Int position)
    {
        bool isPlayerOneOnPosition = position == _playerOnePosition;
        bool isPlayerTwoOnPosition = position == _playerTwoPosition;
        return isPlayerOneOnPosition || isPlayerTwoOnPosition;
    }

    private bool IsInPlayersOffset(Vector2 mousePosition)
    {
        Vector2 playerTwoDownLeftCorner = Camera.main.WorldToScreenPoint(_playerTwoPosition);
        Vector2 playerOneDownLeftCorner = Camera.main.WorldToScreenPoint(_playerOnePosition);
        Vector2 playerSize = GetPlayerSize();

        return IsPositionInOffset(mousePosition, playerOneDownLeftCorner, playerSize) || IsPositionInOffset(mousePosition, playerTwoDownLeftCorner, playerSize);
    }

    private Vector2 GetPlayerSize()
    {
        float playerWidth = Screen.height / 15;
        float playerHeightWithOffset = Screen.height / 8;
        return new Vector2(playerWidth, playerHeightWithOffset);
    }

    private bool IsPositionInOffset(Vector2 position, Vector2 downLeft, Vector2 size)
    {
        Vector2 upRight = downLeft + size;
        return position.x <= upRight.x && position.x >= downLeft.x && position.y >= downLeft.y && position.y <= upRight.y;
    }

    private Vector3Int GetCursorInTilemapPosition()
    {
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return _backgroundTilemap.WorldToCell(cursorWorldPosition);
    }

    


    public void PlayerBeingPlacedHandler(Void data)
    {
        _isPlayerBeingPlaced = true;
    }

    public void PlayerHasBeenPlacedHandler(PlayerInfo playerInfo)
    {
        _isPlayerBeingPlaced = false;
        Vector3Int playerInTilemapPosition = _backgroundTilemap.WorldToCell(playerInfo.WorldPosition);

        if (IsPlayerPositionBlocked(playerInfo.PlayerIndex, playerInTilemapPosition))
        {
            OnInvalidPlayerPosition(playerInfo.PlayerIndex);
            SetPlayerOutsideMap(playerInfo.PlayerIndex);
        }
        else
        {
            UpdatePlayerPosition(playerInfo.PlayerIndex, playerInTilemapPosition);
        }
    }

    private bool IsPlayerPositionBlocked(int playerIndex, Vector3Int position)
    {
        bool arePlayerPositionsMatch = (playerIndex == 1 && position == _playerTwoPosition) || (playerIndex == 2 && position == _playerOnePosition);
        if (arePlayerPositionsMatch)
        {
            return true;
        }
        return !_tilemapBounds.Contains(position) || _obstacleTilemap.HasTile(position);
    }

    private void OnInvalidPlayerPosition(int playerIndex)
    {
        InvalidPlayerPosition.Raise(playerIndex);
    }

    private void UpdatePlayerPosition(int playerIndex, Vector3Int position)
    {   
        if (playerIndex == 1)
        {
            _playerOnePosition = position;
        }
        else
        {
            _playerTwoPosition = position;
        }
    }

    public void ZombieTypeSetHandler(ZombieType zombieType)
    {
        _zombies[zombieType.Type] = zombieType.Count;
    }



    public void OpenUIPanel(GameObject uiPanel)
    {
        _isMenuPanelOpen = true;
        uiPanel.SetActive(true);
    }

    public void ExitUIPanel(GameObject uiPanel)
    {
        _isMenuPanelOpen = false;
        uiPanel.SetActive(false);
    }

    public void SaveButtonHitHandler(Void data)
    {   
        TilemapData tilemapData = ConvertTilemapToData();
        SerializationModel.SaveMap(tilemapData);
    }

    private TilemapData ConvertTilemapToData()
    {
        TilemapData tilemapData = new TilemapData();

        tilemapData.MapName = _loadedMap.TilemapData.MapName;
        tilemapData.PlayerOnePosition = _playerOnePosition;
        tilemapData.PlayerTwoPosition = _playerTwoPosition;
        
        foreach (KeyValuePair<string, int> zombie in _zombies)
        {
            tilemapData.Zombies.Add(new ZombieType(zombie.Key, zombie.Value));
        }

        foreach (Vector3Int pos in _obstacleTilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = _obstacleTilemap.GetTile(pos);
            if (tile != null)
            {
                string tileType = tile.name;
                TileData tileData = new TileData(pos, tileType);
                tilemapData.Tiles.Add(tileData);
            }
        }

        return tilemapData;
    }

    public void DeleteButtonHitHandler(Void data)
    {
        SerializationModel.DeleteMap(_loadedMap.TilemapData.MapName);
        SceneManager.LoadScene("MapSelector");
    }

    public void ExitButtonHitHandler(Void data)
    {
        SceneManager.LoadScene("MapSelector");
    }

    public void WallButtonHit()
    {
        _activeTile = _wallTile;
    }

    public void BrickButtonHit()
    {
        _activeTile = _boxTile;
    }
}
