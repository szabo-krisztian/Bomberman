using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapInitializer : MonoBehaviour
{
    [SerializeField]
    private Tilemap _indestructibles;

    [SerializeField]
    private TilemapSO _tilemapSO;

    [SerializeField]
    private TileBase _wallTile;

    [SerializeField]
    private GameObject _boxPrefab;

    [SerializeField]
    private GameObject _playerOne;

    [SerializeField]
    private GameObject _playerTwo;

    private void Start()
    {
        InitializeTilemap();
        PlacePlayers();
    }

    private void InitializeTilemap()
    {
        foreach (TileData tileData in _tilemapSO.TilemapData.Tiles)
        {
            Vector3Int tilePosition = tileData.Position;
            if (tileData.TileType == "Brick")
            {
                Vector2 boxPosition = UtilityFunctions.GetCenterPosition(tileData.Position);
                Instantiate(_boxPrefab, boxPosition, Quaternion.identity, transform);
            }
            else if (tileData.TileType == "Block")
            {
                _indestructibles.SetTile(tilePosition, _wallTile);
            }
        }
    }

    private void PlacePlayers()
    {
        _playerOne.transform.position = UtilityFunctions.GetCenterPosition(_tilemapSO.TilemapData.PlayerOnePosition);
        _playerTwo.transform.position = UtilityFunctions.GetCenterPosition(_tilemapSO.TilemapData.PlayerTwoPosition);
    }
}
