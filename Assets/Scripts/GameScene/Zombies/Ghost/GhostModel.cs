using UnityEngine;

public class GhostModel
{
    private CollisionDetectionModel _collisionDetector;

    public GhostModel()
    {
        _collisionDetector = new CollisionDetectionModel();
    }

    /// <summary>
    /// We loop through the walls and check if there is a hole or not.
    /// </summary>
    /// <param name="direction">Direction of the zombie.</param>
    /// <param name="position">World position of the zombie.</param>
    /// <returns></returns>
    public Vector3 GetPivotPoint(Vector3 direction, Vector3 position)
    {
        Vector3 newPosition = GetNewPosition(position, direction);

        while (UtilityFunctions.IsPositionInMap(UtilityFunctions.GetTilemapPosition(newPosition)) && !IsFreeSpaceFound(newPosition))
        {
            newPosition = GetNewPosition(newPosition, direction);
        }

        return newPosition;
    }

    private Vector3 GetNewPosition(Vector3 position, Vector3 direction)
    {
        return position + direction;
    }

    /// <summary>
    /// We found a hole in the walls.
    /// </summary>
    /// <param name="position">Given world position of a tile.</param>
    /// <returns>boolean</returns>
    private bool IsFreeSpaceFound(Vector3 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return !_collisionDetector.IsTagInColliders(colliders, "Box") && !_collisionDetector.IsTagInColliders(colliders, "Wall");
    }

    /// <summary>
    /// We are delaying the state transition if the zombie came out of the wall but is standing in a bomb.
    /// </summary>
    /// <param name="position">Given world position of a tile.</param>
    /// <returns>boolean</returns>
    public bool IsZombieStandingInBomb(Vector3 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return _collisionDetector.IsTagInColliders(colliders, "Bomb");
    }
}
