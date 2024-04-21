using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    private GameObject[] _zombieEggs;

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
            for (int i = 0; i < zombieType.Count; i++)
            {
                var pos = freePositions.Dequeue();

                switch (zombieType.Type)
                {
                    case "Normal":
                        Instantiate(_zombieEggs[0], pos, Quaternion.identity);
                        break;
                    case "Ghost":
                        Instantiate(_zombieEggs[1], pos, Quaternion.identity);
                        break;
                    case "Intelligent":
                        Instantiate(_zombieEggs[2], pos, Quaternion.identity);
                        break;
                    case "VeryIntelligent":
                        Instantiate(_zombieEggs[3], pos, Quaternion.identity);
                        break;
                }
            }
        }
    }

    public void EggCrackedHandler(EggCrackedInfo info)
    {
        Instantiate(info.Dino, info.Position, Quaternion.identity);
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
