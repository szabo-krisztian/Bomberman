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

    [SerializeField]
    private Transform _entityGroup;

    private CollisionDetectionModel _collisionDetector = new CollisionDetectionModel();

    public void NewGameStartedHandler(Void data)
    {
        KillAllEntities();
        InitializeTilemap();
        PlacePlayers();
        PlaceZombies();
    }
    
    private void KillAllEntities()
    {
        foreach (Transform child in _entityGroup)
        {
            Destroy(child.gameObject);
        }
    }

    private void InitializeTilemap()
    {
        foreach (TileData tileData in _tilemapSO.TilemapData.Tiles)
        {
            Vector3 worldPosition = UtilityFunctions.GetCenterPosition(tileData.Position);

            if (tileData.TileType == "Brick")
            {
                var gO = Instantiate(_blockPrefabs[0], worldPosition, Quaternion.identity, _entityGroup);
                gO.GetComponent<BoxController>().SetEntityGroup(_entityGroup);
            }
            else if (tileData.TileType == "Block")
            {
                Instantiate(_blockPrefabs[1], worldPosition, Quaternion.identity, _entityGroup);
            }
        }
    }

    private void PlacePlayers()
    {
        GameObject player1 = Instantiate(_playerPrefabs[0], UtilityFunctions.GetCenterPosition(_tilemapSO.TilemapData.PlayerOnePosition), Quaternion.identity, _entityGroup);
        player1.name = "Player1";
        GameObject player2 = Instantiate(_playerPrefabs[1], UtilityFunctions.GetCenterPosition(_tilemapSO.TilemapData.PlayerTwoPosition), Quaternion.identity, _entityGroup);
        player2.name = "Player2";
    }

    private void PlaceZombies()
    {
        List<Vector3Int> allTilePositions = UtilityFunctions.GetAllTilePositionsInTilemap();
        allTilePositions.Shuffle();
        Stack<Vector3> freePositions = new Stack<Vector3>();

        foreach (Vector3Int position in allTilePositions)
        {
            Vector3 worldPosition = UtilityFunctions.GetCenterPosition(position);

            if (IsFreeSpace(worldPosition))
            {
                freePositions.Push(worldPosition);
            }
        }

        foreach (ZombieType zombieType in _tilemapSO.TilemapData.Zombies)
        {
            for (int i = 0; i < zombieType.Count; i++)
            {
                if (freePositions.Count == 0)
                {
                    break;
                }

                var pos = freePositions.Pop();

                switch (zombieType.Type)
                {
                    case "Normal":
                        SummonZombieEgg(0, pos);
                        break;
                    case "Ghost":
                        SummonZombieEgg(1, pos);
                        break;
                    case "Intelligent":
                        SummonZombieEgg(2, pos);
                        break;
                    case "VeryIntelligent":
                        SummonZombieEgg(3, pos);
                        break;
                }
            }
        }
    }

    private void SummonZombieEgg(int zombieIndex, Vector3 position)
    {
        Instantiate(_zombieEggs[zombieIndex], position, Quaternion.identity, _entityGroup);
    }

    public void EggCrackedHandler(EggCrackedInfo info)
    {
        Instantiate(info.Dino, info.Position, Quaternion.identity, _entityGroup);
    }

    private bool IsFreeSpace(Vector3 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return !_collisionDetector.IsTagInColliders(colliders, "Wall") && !_collisionDetector.IsTagInColliders(colliders, "Box") && !_collisionDetector.IsTagInColliders(colliders, "Player");
    }

    public void FreezeGame(Void data)
    {
        foreach (Transform child in _entityGroup)
        {
            if (child.gameObject.tag != "Wall" && child.gameObject.tag != "Box")
            {
                Destroy(child.gameObject);
            }
        }
    }
}
