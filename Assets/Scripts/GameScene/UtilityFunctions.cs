using System.Collections.Generic;
using UnityEngine;

public static class UtilityFunctions
{
    public static readonly List<Vector2Int> Directions = new List<Vector2Int>() { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
    public static readonly Vector2Int MAP_UPPER_LEFT_CORNER = new Vector2Int(-7, 7);
    public static readonly Vector2Int MAP_DOWN_RIGHT_CORNER = new Vector2Int(7, -6);
    public static readonly Vector2Int NULL_VECTOR = new Vector2Int(MAP_DOWN_RIGHT_CORNER.x + 1, MAP_DOWN_RIGHT_CORNER.y - 1);

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

    public static bool IsPositionInMap(Vector3 position)
    {
        return position.x >= MAP_UPPER_LEFT_CORNER.x && position.x <= MAP_DOWN_RIGHT_CORNER.x &&
            position.y <= MAP_UPPER_LEFT_CORNER.y && position.y >= MAP_DOWN_RIGHT_CORNER.y;
    }
}
