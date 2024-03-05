using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TilemapData
{
    public List<TileData> Tiles;
    public Vector3Int[] PlayerPositions;
    public Dictionary<string, int> Zombies;

    public TilemapData(List<TileData> tiles, Vector3Int[] playerPositions, Dictionary<string, int> zombies)
    {
        Tiles = tiles;
        PlayerPositions = playerPositions;
        Zombies = zombies;
    }

    public TilemapData()
    {
        Tiles = new List<TileData>();
        PlayerPositions = new Vector3Int[3];
        Zombies = new Dictionary<string, int>();
    }
}
