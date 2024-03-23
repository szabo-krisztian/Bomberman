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
    private TileBase[] _tiles;

    private Tilemap _obstacles;
    private Tilemap _background;
    private TileBase _activeTile;
    

    private void Start()
    {
        Tilemap[] tilemaps = GetComponentsInChildren<Tilemap>();
        _obstacles = tilemaps[0];
        _background = tilemaps[1];
    }


    private void Update()
    {
        Vector3Int cursorTilemapPosition = GetCursorInTilemapPosition();
        BoundsInt bounds = _background.cellBounds;

        if (bounds.Contains(cursorTilemapPosition))
        {
            Debug.Log("Cursor in tilemap");
        }
    }

    private Vector3Int GetCursorInTilemapPosition()
    {
        Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return _background.WorldToCell(cursorWorldPos);
    }
}
