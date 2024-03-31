using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TilemapData
{
    public List<TileData> Tiles;
    public Vector3Int PlayerOnePosition;
    public Vector3Int PlayerTwoPosition;
    public string MapName;
    public List<ZombieType> Zombies;

    public TilemapData(List<TileData> tiles, Vector3Int playerOne, Vector3Int playerTwo, List<ZombieType> zombies)
    {
        Tiles = tiles;
        PlayerOnePosition = playerOne;
        PlayerTwoPosition = playerTwo;
        Zombies = zombies;
    }

    public TilemapData()
    {
        Tiles = new List<TileData>();
        PlayerOnePosition = new Vector3Int(-69, -69, -69);
        PlayerTwoPosition = new Vector3Int(-69, -69, -69);
        Zombies = new List<ZombieType>();
    }
}
