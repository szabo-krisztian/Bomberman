using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapInitializer : MonoBehaviour
{
    [SerializeField]
    private TilemapSO _tilemapSO;

    [SerializeField]
    private GameObject[] _blockPrefabs;

    [SerializeField]
    private GameObject[] _playerPrefabs;

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
            Vector3 worldPosition = UtilityFunctions.GetCenterPosition(tileData.Position);

            if (tileData.TileType == "Brick")
            {
                Instantiate(_blockPrefabs[0], worldPosition, Quaternion.identity, transform);
            }
            else if (tileData.TileType == "Block")
            {
                Instantiate(_blockPrefabs[1], worldPosition, Quaternion.identity, transform);
            }
        }
    }

    private void PlacePlayers()
    {
        Instantiate(_playerPrefabs[0], UtilityFunctions.GetCenterPosition(_tilemapSO.TilemapData.PlayerOnePosition), Quaternion.identity);
        Instantiate(_playerPrefabs[1], UtilityFunctions.GetCenterPosition(_tilemapSO.TilemapData.PlayerTwoPosition), Quaternion.identity);
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
        foreach (var k in colliders)
        {
            //Debug.Log(k.tag);
        }
        return (!_collisionDetector.IsTagInColliders(colliders, "Wall") && !_collisionDetector.IsTagInColliders(colliders, "Box"));
    }
}
