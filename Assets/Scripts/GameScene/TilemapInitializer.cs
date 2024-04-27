using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TilemapInitializer : MonoBehaviour
{
    [SerializeField]
    private TilemapSO _tilemapSO;

    [SerializeField]
    private GameObject[] _blockPrefabs;

    [SerializeField]
    private GameObject[] _dustParticles;

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
        StartCoroutine(InitializeTilemap());
    }
    
    private void KillAllEntities()
    {
        foreach (Transform child in _entityGroup)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator InitializeTilemap()
    {
        List<TileData> tiles = _tilemapSO.TilemapData.Tiles;
        tiles.Shuffle();

        yield return StartCoroutine(PlaceObstacles(tiles));
        yield return StartCoroutine(PlaceZombies());
        PlacePlayers();
    }

    private IEnumerator PlaceObstacles(List<TileData> tiles)
    {
        foreach (TileData tileData in tiles)
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

            Instantiate(_dustParticles[0], worldPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.009f);
        }
    }

    private IEnumerator PlaceZombies()
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
            yield return new WaitForSeconds(.5f);
        }
    }

    private void PlacePlayers()
    {
        Vector3 playerOneWorldPosition = UtilityFunctions.GetCenterPosition(_tilemapSO.TilemapData.PlayerOnePosition);
        Vector3 playerTwoWorldPosition = UtilityFunctions.GetCenterPosition(_tilemapSO.TilemapData.PlayerTwoPosition);

        GameObject player1 = Instantiate(_playerPrefabs[0], playerOneWorldPosition, Quaternion.identity, _entityGroup);
        player1.name = "Player1";
        Instantiate(_dustParticles[1], playerOneWorldPosition, Quaternion.identity);

        GameObject player2 = Instantiate(_playerPrefabs[1], playerTwoWorldPosition, Quaternion.identity, _entityGroup);
        player2.name = "Player2";
        Instantiate(_dustParticles[1], playerTwoWorldPosition, Quaternion.identity);
    }

    private void SummonZombieEgg(int zombieIndex, Vector3 position)
    {
        Instantiate(_zombieEggs[zombieIndex], position, Quaternion.identity, _entityGroup);
        Instantiate(_dustParticles[1], position, Quaternion.identity);
    }

    public void EggCrackedHandler(EggCrackedInfo info)
    {
        Instantiate(info.Dino, info.Position, Quaternion.identity, _entityGroup);
    }

    private bool IsFreeSpace(Vector3 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return !_collisionDetector.IsTagInColliders(colliders, "Wall") &&
               !_collisionDetector.IsTagInColliders(colliders, "Box") &&
               !(UtilityFunctions.GetTilemapPosition(position) == _tilemapSO.TilemapData.PlayerTwoPosition) &&
               !(UtilityFunctions.GetTilemapPosition(position) == _tilemapSO.TilemapData.PlayerOnePosition);
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
