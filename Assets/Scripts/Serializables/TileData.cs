using UnityEngine;


/// <summary>
/// Serializable data for storing tiles
/// </summary>
[System.Serializable]
public class TileData
{
    public Vector3Int Position;
    public string TileType;

    public TileData(Vector3Int position, string tileType)
    {
        Position = position;
        TileType = tileType;
    }

    public TileData() { }
}