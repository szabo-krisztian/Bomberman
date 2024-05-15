using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEditorManager : MonoBehaviour
{
    public GameEvent<string> LoadScene;
    public GameEvent<int> InvalidPlayerPosition;

    [SerializeField]
    private GameObject _wallPrefab;

    [SerializeField]
    private GameObject _boxPrefab;

    [SerializeField]
    private GameObject _dirtPrefab;

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
    private GameObject _activePrefab;
    private GameObject _currentPrefab;
    private bool _isPlayerBeingPlaced;
    private bool _isMenuPanelOpen;

    private Vector3Int _playerOnePosition;
    private Vector3Int _playerTwoPosition;

    private Dictionary<string, int> _zombies;

    /// <summary>
    /// Built-in Unity method. We initiliaze our Tilemap, set the starting attributes for UI elements.
    /// </summary>
    private void Start()
    {
        Tilemap[] tilemaps = GetComponentsInChildren<Tilemap>();
        _obstacleTilemap = tilemaps[0];
        _backgroundTilemap = tilemaps[1];

        _tilemapBounds = GetTilemapBounds();
        _activeTile = _wallTile;
        _activePrefab = _wallPrefab;
        _isPlayerBeingPlaced = false;
        _isMenuPanelOpen = false;

        SetPlayerOutsideMap(1);
        SetPlayerOutsideMap(2);
        InitZombies();

        InitializeTilemap();
    }

    /// <summary>
    /// Helper method querying the bounds of our Tilemap.
    /// </summary>
    /// <returns></returns>
    private BoundsInt GetTilemapBounds()
    {
        BoundsInt tilemapBounds = _backgroundTilemap.cellBounds;

        tilemapBounds.xMin += 1;
        tilemapBounds.xMax -= 1;
        tilemapBounds.yMin += 1;
        tilemapBounds.yMax -= 1;

        return tilemapBounds;
    }

    /// <summary>
    /// Helper method for setting players outside the map. Helpful when the user drags a player UI elemnt outside the map.
    /// </summary>
    /// <param name="playerIndex">Corresponding index of a player.</param>
    private void SetPlayerOutsideMap(int playerIndex)
    {
        if (playerIndex == 1)
        {
            _playerOnePosition = _loadedMap.TilemapData.NULL_POSITION;
        }
        else
        {
            _playerTwoPosition = _loadedMap.TilemapData.NULL_POSITION;
        }
    }

    /// <summary>
    /// Helper method that sets the basic values of the zombie sliders.
    /// </summary>
    private void InitZombies()
    {
        _zombies = new Dictionary<string, int>();
        _zombies["Normal"] = 0;
        _zombies["Ghost"] = 0;
        _zombies["Intelligent"] = 0;
        _zombies["VeryIntelligent"] = 0;
    }


    /// <summary>
    /// Helper method for populating the tilemap with the bricks and walls.
    /// </summary>
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

    /// <summary>
    /// Built-in Unity method that calls each frame. We handle the user mouse inputs. 
    /// </summary>
    private void Update()
    {
        ClearPreviousPrefab();

        Vector3Int cursorTilemapPosition = GetCursorInTilemapPosition();
        if (!IsUserAbleToPlaceTiles(cursorTilemapPosition))
        {
            return;
        }

        if (Input.GetMouseButton(0) && _activeTile != _obstacleTilemap.GetTile(cursorTilemapPosition))
        {
            _obstacleTilemap.SetTile(cursorTilemapPosition, _activeTile);
            Instantiate(_dirtPrefab, UtilityFunctions.GetCenterPosition(cursorTilemapPosition), Quaternion.identity);
        }

        if (Input.GetMouseButton(1))
        {
            _obstacleTilemap.SetTile(cursorTilemapPosition, null);
        }
        else
        {
            _currentPrefab = Instantiate(_activePrefab, UtilityFunctions.GetCenterPosition(cursorTilemapPosition), Quaternion.identity);
        }
    }

    /// <summary>
    /// We check if the player can place tiles at the moment. This returns false if the user clicks outside the map for example.
    /// </summary>
    /// <param name="position">World position of a tile that we got from the user click.</param>
    /// <returns>boolean</returns>
    private bool IsUserAbleToPlaceTiles(Vector3Int position)
    {
        return !_isPlayerBeingPlaced && !_isMenuPanelOpen && _tilemapBounds.Contains(position) && !ArePlayersOnPosition(position) && !IsInPlayersOffset(Input.mousePosition);
    }

    /// <summary>
    /// Helper method that returns true if player stands on a given position.
    /// </summary>
    /// <param name="position">World position of a tile.</param>
    /// <returns>boolean</returns>
    private bool ArePlayersOnPosition(Vector3Int position)
    {
        bool isPlayerOneOnPosition = position == _playerOnePosition;
        bool isPlayerTwoOnPosition = position == _playerTwoPosition;
        return isPlayerOneOnPosition || isPlayerTwoOnPosition;
    }

    /// <summary>
    /// User cannot place blocks if their cursor is positioned in the offset of either players' head.
    /// </summary>
    /// <param name="mousePosition">Screen position of the cursor.</param>
    /// <returns>boolean</returns>
    private bool IsInPlayersOffset(Vector2 mousePosition)
    {
        Vector2 playerTwoDownLeftCorner = Camera.main.WorldToScreenPoint(_playerTwoPosition);
        Vector2 playerOneDownLeftCorner = Camera.main.WorldToScreenPoint(_playerOnePosition);
        Vector2 playerSize = GetPlayerSize();

        return IsPositionInOffset(mousePosition, playerOneDownLeftCorner, playerSize) || IsPositionInOffset(mousePosition, playerTwoDownLeftCorner, playerSize);
    }

    /// <summary>
    /// Helper method for clearing the blank square prefab. This is just a design idea.
    /// </summary>
    private void ClearPreviousPrefab()
    {
        if (_currentPrefab != null)
        {
            Destroy(_currentPrefab);
        }
    }

    /// <summary>
    /// Helper method querying the size of the player.
    /// </summary>
    /// <returns>Float vector</returns>
    private Vector2 GetPlayerSize()
    {
        float playerWidth = Screen.height / 15;
        float playerHeightWithOffset = Screen.height / 10;
        return new Vector2(playerWidth, playerHeightWithOffset);
    }

    /// <summary>
    /// Checks if the given position overlaps the offset of either players' head.
    /// </summary>
    /// <param name="position">World cursor position.</param>
    /// <param name="downLeft">Down left coordinate of the player's UI element.</param>
    /// <param name="size">Size of the player's UI element.</param>
    /// <returns>boolean</returns>
    private bool IsPositionInOffset(Vector2 position, Vector2 downLeft, Vector2 size)
    {
        Vector2 upRight = downLeft + size;
        return position.x <= upRight.x && position.x >= downLeft.x && position.y >= downLeft.y && position.y <= upRight.y;
    }

    /// <summary>
    /// Get the world position of the cursor.
    /// </summary>
    /// <returns>World cursor position.</returns>
    private Vector3Int GetCursorInTilemapPosition()
    {
        Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return _backgroundTilemap.WorldToCell(cursorWorldPosition);
    }

    /// <summary>
    /// Event handler method that calls if either player is being placed.
    /// </summary>
    /// <param name="data">Event parameter.</param>
    public void PlayerBeingPlacedHandler(Void data)
    {
        _isPlayerBeingPlaced = true;
    }

    /// <summary>
    /// Event handler method that calls if either player has been placed.
    /// </summary>
    /// <param name="playerInfo">Custom event parameter containing the index and position of the player.</param>
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

    /// <summary>
    /// Returns true if the user cannot place the player on a given position.
    /// </summary>
    /// <param name="playerIndex">Index of a player.</param>
    /// <param name="position">World positition of the player.</param>
    /// <returns>boolean</returns>
    private bool IsPlayerPositionBlocked(int playerIndex, Vector3Int position)
    {
        bool doPlayerPositionsMatch = (playerIndex == 1 && position == _playerTwoPosition) || (playerIndex == 2 && position == _playerOnePosition);
        if (doPlayerPositionsMatch)
        {
            return true;
        }
        return !_tilemapBounds.Contains(position) || _obstacleTilemap.HasTile(position);
    }

    /// <summary>
    /// Event handler method that calls if a player's placement is invalid.
    /// </summary>
    /// <param name="playerIndex">Index of our player.</param>
    private void OnInvalidPlayerPosition(int playerIndex)
    {
        InvalidPlayerPosition.Raise(playerIndex);
    }

    /// <summary>
    /// Helper method for setting the players on a given position.
    /// </summary>
    /// <param name="playerIndex">Index of a player.</param>
    /// <param name="position">New world position.</param>
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

    /// <summary>
    /// Event handler method that calls if the user has set the count of a zombie.
    /// </summary>
    /// <param name="zombieType">Custom event parameter that contains the type and count of a zombie.</param>
    public void ZombieTypeSetHandler(ZombieType zombieType)
    {
        _zombies[zombieType.Type] = zombieType.Count;
    }

    /// <summary>
    /// Helper method that opens a given uiPanel, pop-up window.
    /// </summary>
    /// <param name="uiPanel">UI panel, pop-up window.</param>
    public void OpenUIPanel(GameObject uiPanel)
    {
        _isMenuPanelOpen = true;
        uiPanel.SetActive(true);
    }

    /// <summary>
    /// Helper method that closes a given uiPanel, pop-up window.
    /// </summary>
    /// <param name="uiPanel">UI panel, pop-up window.</param>
    public void ExitUIPanel(GameObject uiPanel)
    {
        _isMenuPanelOpen = false;
        uiPanel.SetActive(false);
    }

    /// <summary>
    /// Event handler method that calls if the save button has been pressed.
    /// </summary>
    /// <param name="data">Event parameter.</param>
    public void SaveButtonHitHandler(Void data)
    {   
        if (ArePlayersPlaced())
        {
            TilemapData tilemapData = ConvertTilemapToData();
            SerializationModel.SaveMap(tilemapData);
            LoadScene.Raise("MapSelector");
        }
    }

    /// <summary>
    /// Returns true if both players are in the tilemap.
    /// </summary>
    /// <returns>boolean</returns>
    private bool ArePlayersPlaced()
    {
        return _playerOnePosition != _loadedMap.TilemapData.NULL_POSITION && _playerTwoPosition != _loadedMap.TilemapData.NULL_POSITION;
    }

    /// <summary>
    /// Helper method that prepares the saving.
    /// </summary>
    /// <returns>The serializable data we want to save.</returns>
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

    /// <summary>
    /// Event handler method that calls if the delete button has been pressed. We delete the map and exit the scene.
    /// </summary>
    /// <param name="data">Event parameter.</param>
    public void DeleteButtonHitHandler(Void data)
    {
        SerializationModel.DeleteMap(_loadedMap.TilemapData.MapName);
        LoadScene.Raise("MapSelector");
    }

    /// <summary>
    /// Event handler method that calls if the user has pressed the exit button. We exit the scene and go back to the menu.
    /// </summary>
    /// <param name="data">Event parameter.</param>
    public void SceneExitButtonHitHandler(Void data)
    {
        LoadScene.Raise("MapSelector");
    }

    /// <summary>
    /// Event handler method that sets current block type to wall.
    /// </summary>
    public void WallButtonHit()
    {
        _activeTile = _wallTile;
        _activePrefab = _wallPrefab;
    }

    /// <summary>
    /// Event handler method that sets current block type to brick.
    /// </summary>
    public void BrickButtonHit()
    {
        _activeTile = _boxTile;
        _activePrefab = _boxPrefab;
    }
}
