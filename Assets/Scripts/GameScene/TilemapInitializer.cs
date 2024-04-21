using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapInitializer : MonoBehaviour
{
    [SerializeField]
    private TilemapSO _tilemapSO;

    [SerializeField]
    private Tilemap _indestructibles;   

    [SerializeField]
    private TileBase _wallTile;

    [SerializeField]
    private GameObject _boxPrefab;

    [SerializeField]
    private GameObject _playerOne;

    [SerializeField]
    private GameObject _playerTwo;

    [SerializeField]
    private GameObject[] _zombies;

    private CollisionDetectionModel _collisionDetector;

    private void Start()
    {
        _collisionDetector = new CollisionDetectionModel();
        InitializeTilemap();
        PlacePlayers();
        PlaceZombies();
    }

    private void InitializeTilemap()
    {
        foreach (TileData tileData in _tilemapSO.TilemapData.Tiles)
        {
            Vector3Int tilePosition = tileData.Position;
            if (tileData.TileType == "Brick")
            {
                Vector3 boxPosition = UtilityFunctions.GetCenterPosition(tileData.Position);
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
        Instantiate(_playerOne, UtilityFunctions.GetCenterPosition(_tilemapSO.TilemapData.PlayerOnePosition), Quaternion.identity);
        Instantiate(_playerTwo, UtilityFunctions.GetCenterPosition(_tilemapSO.TilemapData.PlayerTwoPosition), Quaternion.identity);
    }

    private void PlaceZombies()
    {
        List<Vector3Int> allTilePositions = UtilityFunctions.GetAllTilePositionsInTilemap();
        allTilePositions.Shuffle();
        Queue<Vector3> freePositions = new Queue<Vector3>();

        foreach (Vector3Int position in allTilePositions)
        {
            Vector3 worldPosition = UtilityFunctions.GetCenterPosition(position);
            if (IsFreeSpace(worldPosition))
            {
                freePositions.Enqueue(worldPosition);
            }
        }

        foreach (ZombieType zombieType in _tilemapSO.TilemapData.Zombies)
        {
            switch (zombieType.Type)
            {
                case "Normal":
                    Instantiate(_zombies[0], freePositions.Dequeue(), Quaternion.identity);
                    break;
                case "Ghost":
                    Instantiate(_zombies[1], freePositions.Dequeue(), Quaternion.identity);
                    break;
                case "Intelligent":
                    Instantiate(_zombies[2], freePositions.Dequeue(), Quaternion.identity);
                    break;
                case "VeryIntelligent":
                    Instantiate(_zombies[3], freePositions.Dequeue(), Quaternion.identity);
                    break;
            }
        }
    }

    private bool IsFreeSpace(Vector3 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return !_collisionDetector.IsTagInColliders(colliders, "Box") && !_collisionDetector.IsTagInColliders(colliders, "Wall");
    }
}
