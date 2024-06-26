using System.Collections.Generic;
using UnityEngine;

public static class UtilityFunctions
{
    public static readonly List<Vector3Int> Directions = new List<Vector3Int>()
    {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
    };

    public static readonly Vector3Int MAP_UPPER_LEFT_CORNER = new Vector3Int(-7, 6);
    public static readonly Vector3Int MAP_DOWN_RIGHT_CORNER = new Vector3Int(5, -6);
    public static readonly Vector3Int NULL_VECTOR = new Vector3Int(MAP_DOWN_RIGHT_CORNER.x + 1, MAP_DOWN_RIGHT_CORNER.y - 1);

    public static Vector3 GetCenterPosition(Vector3 position)
    {
        return new Vector3(Mathf.FloorToInt(position.x) + .5f, Mathf.FloorToInt(position.y) + .5f);
    }

    public static Vector3Int GetTilemapPosition(Vector3 position)
    {
        return new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0);
    }

    public static bool IsPositionInMap(Vector3Int position)
    {
        return position.x >= MAP_UPPER_LEFT_CORNER.x &&
               position.x <= MAP_DOWN_RIGHT_CORNER.x &&
               position.y <= MAP_UPPER_LEFT_CORNER.y &&
               position.y >= MAP_DOWN_RIGHT_CORNER.y;
    }

    public static List<Vector3Int> GetAllTilePositionsInTilemap()
    {
        List<Vector3Int> positions = new List<Vector3Int>();

        for (int i = MAP_UPPER_LEFT_CORNER.x; i <= MAP_DOWN_RIGHT_CORNER.x; ++i)
        {
            for (int j = MAP_UPPER_LEFT_CORNER.y; j >= MAP_DOWN_RIGHT_CORNER.y; --j)
            {
                positions.Add(new Vector3Int(i, j));
            }
        }

        return positions;
    }

    public static bool AreVectorsAligned(Vector3 vectorA, Vector3 vectorB)
    {
        return Vector3.Dot(vectorA.normalized, vectorB.normalized) >= 0.99f;
    }
}
