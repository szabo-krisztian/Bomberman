using System.Collections.Generic;
using UnityEngine;

public static class UtilityFunctions
{
    public static readonly List<Vector2Int> Directions = new List<Vector2Int>() { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    public static Vector2 GetCenterPosition(Vector3 position)
    {

        return new Vector2(Mathf.FloorToInt(position.x) + .5f, Mathf.FloorToInt(position.y) + .5f);
    }

    public static Vector2 GetCenterPosition(Vector2 position)
    {

        return new Vector2(Mathf.FloorToInt(position.x) + .5f, Mathf.FloorToInt(position.y) + .5f);
    }

    public static Vector3Int GetTilemapPosition(Vector3 position)
    {
        return new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0);
    }

    public static Vector2Int GetTilemapPosition(Vector2 position)
    {
        return new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
    }
}
