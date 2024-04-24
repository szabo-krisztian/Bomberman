using UnityEngine;

public class GhostModel
{
    private CollisionDetectionModel _collisionDetector;

    public GhostModel()
    {
        _collisionDetector = new CollisionDetectionModel();
    }

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

    private bool IsFreeSpaceFound(Vector3 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return !_collisionDetector.IsTagInColliders(colliders, "Box") && !_collisionDetector.IsTagInColliders(colliders, "Wall");
    }

    public bool IsZombieStandingInBomb(Vector3 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return _collisionDetector.IsTagInColliders(colliders, "Bomb");
    }
}
