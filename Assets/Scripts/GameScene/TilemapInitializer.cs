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
    private Transform _entitiesGroup;

    private CollisionDetectionModel _collisionDetector;

    private void Start()
    {
        _collisionDetector = new CollisionDetectionModel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        KillAllEntities();
        InitializeTilemap();
        PlacePlayers();
        PlaceZombies();
    }
    
    private void KillAllEntities()
    {
        foreach (Transform child in _entitiesGroup)
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
                Instantiate(_blockPrefabs[0], worldPosition, Quaternion.identity, _entitiesGroup);
            }
            else if (tileData.TileType == "Block")
            {
                Instantiate(_blockPrefabs[1], worldPosition, Quaternion.identity, _entitiesGroup);
            }
        }
    }

    private void PlacePlayers()
    {
        GameObject player1 = Instantiate(_playerPrefabs[0], UtilityFunctions.GetCenterPosition(_tilemapSO.TilemapData.PlayerOnePosition), Quaternion.identity, _entitiesGroup);
        player1.name = "Player1";
        GameObject player2 = Instantiate(_playerPrefabs[1], UtilityFunctions.GetCenterPosition(_tilemapSO.TilemapData.PlayerTwoPosition), Quaternion.identity, _entitiesGroup);
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
        Instantiate(_zombieEggs[zombieIndex], position, Quaternion.identity, _entitiesGroup);
    }

    public void EggCrackedHandler(EggCrackedInfo info)
    {
        Instantiate(info.Dino, info.Position, Quaternion.identity, _entitiesGroup);
    }

    private bool IsFreeSpace(Vector3 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return (!_collisionDetector.IsTagInColliders(colliders, "Wall") && !_collisionDetector.IsTagInColliders(colliders, "Box"));
    }
}
