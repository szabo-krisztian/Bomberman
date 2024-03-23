using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEditorManager : MonoBehaviour
{
    [SerializeField]
    private TilemapSO _loadedMap;

    [SerializeField]
    private GameObject _wallPrefab;

    [SerializeField]
    private GameObject _brickPrefab;

    [SerializeField]
    private TileBase _wallTile;

    [SerializeField]
    private TileBase _boxTile;

    private Tilemap _obstacles;
    private Tilemap _background;
    private BoundsInt _tilemapBounds;

    private TileBase _activeTile;

    private void Start()
    {
        Tilemap[] tilemaps = GetComponentsInChildren<Tilemap>();
        _obstacles = tilemaps[0];
        _background = tilemaps[1];
        SetTilemapBounds();

        _activeTile = _boxTile;

        InitializeTilemap();
    }

    private void SetTilemapBounds()
    {
        _tilemapBounds = _background.cellBounds;
        _tilemapBounds.xMin += 1;
        _tilemapBounds.xMax -= 1;
        _tilemapBounds.yMin += 1;
        _tilemapBounds.yMax -= 1;
    }

    private void InitializeTilemap()
    {
        foreach (TileData tileData in _loadedMap.TilemapData.Tiles)
        {
            Vector3Int tilePosition = tileData.Position;
            if (tileData.TileType == "Brick")
            {
                _obstacles.SetTile(tilePosition, _boxTile);
            }
            else if (tileData.TileType == "Block")
            {
                _obstacles.SetTile(tilePosition, _wallTile);
            }
        }
    }

    private void Update()
    {
        Vector3Int cursorTilemapPosition = GetCursorInTilemapPosition();

        if (Input.GetMouseButton(0) && _tilemapBounds.Contains(cursorTilemapPosition))
        {
            _obstacles.SetTile(cursorTilemapPosition, _activeTile);
        }
    }

    private Vector3Int GetCursorInTilemapPosition()
    {
        Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return _background.WorldToCell(cursorWorldPos);
    }
}
