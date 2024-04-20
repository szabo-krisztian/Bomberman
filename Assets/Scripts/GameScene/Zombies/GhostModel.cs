using UnityEngine;

public class GhostModel
{
    private CollisionDetectionModel _collisionDetector;

    public GhostModel()
    {
        _collisionDetector = new CollisionDetectionModel();
    }

    public Vector3 GetPivotPoint(Vector2 direction, Vector3 position)
    {
        Vector3 newPosition = GetNewPosition(position, direction);
        while (UtilityFunctions.IsPositionInMap(newPosition) && !IsFreeSpaceFound(newPosition))
        {
            Debug.Log(newPosition);
            newPosition = GetNewPosition(newPosition, direction);
        }

        return newPosition;
    }

    private Vector3 GetNewPosition(Vector3 position, Vector2 direction)
    {
        return new Vector3(position.x + direction.x, position.y + direction.y, 0);
    }

    private bool IsFreeSpaceFound(Vector3 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return colliders.Length == 0 || _collisionDetector.IsTagInColliders(colliders, "Bomb");
    }
}
