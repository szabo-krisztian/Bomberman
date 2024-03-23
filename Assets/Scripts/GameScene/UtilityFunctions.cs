using UnityEngine;

public static class UtilityFunctions
{
    public static Vector2 GetCenterPosition(Vector3 position)
    {

        return new Vector2(Mathf.FloorToInt(position.x) + .5f, Mathf.FloorToInt(position.y) + .5f);
    }

    public static Vector3Int GetTilemapPosition(Vector3 position)
    {
        return new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0);
    }
}
