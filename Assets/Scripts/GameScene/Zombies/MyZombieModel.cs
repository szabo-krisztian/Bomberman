using System.Collections.Generic;
using UnityEngine;

public class MyZombieModel
{
    protected readonly System.Random random;
    protected readonly CollisionDetectionModel _collisionDetector;

    public MyZombieModel()
    {
        random = new System.Random();
        _collisionDetector = new CollisionDetectionModel();
    }

    protected bool IsPositionFree(Vector2 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return (!_collisionDetector.IsTagInColliders(colliders, "Wall") && !_collisionDetector.IsTagInColliders(colliders, "Box"));
    }

    public List<Vector2Int> GetFreeNeighbourPositions(Vector2Int position)
    {
        List<Vector2Int> freeNeighbours = new List<Vector2Int>();

        foreach (Vector2 direction in UtilityFunctions.Directions)
        {
            Vector2 neighbourWorldPosition = UtilityFunctions.GetCenterPosition(position) + direction;
            if (IsPositionFree(neighbourWorldPosition))
            {
                freeNeighbours.Add(UtilityFunctions.GetTilemapPosition(neighbourWorldPosition));
            }
        }

        return freeNeighbours;
    }

    public List<Vector2Int> GetFreeNeighbourDirections(Vector2Int position)
    {
        List<Vector2Int> freeDirections = new List<Vector2Int>();

        foreach (Vector2Int freeNeighbourPosition in GetFreeNeighbourPositions(position))
        {
            freeDirections.Add(freeNeighbourPosition - position);
        }

        return freeDirections;
    }

    public Vector2Int GetRandomDirection(Vector2 position, Vector2 facingDirection)
    {
        List<Vector2Int> availableDirections = GetFreeNeighbourDirections(UtilityFunctions.GetTilemapPosition(position));
        if (availableDirections.Count == 0)
        {
            return Vector2Int.zero;
        }

        Vector2Int randomDirection = availableDirections[random.Next(0, availableDirections.Count)];
        while (randomDirection == facingDirection)
        {
            randomDirection = availableDirections[random.Next(0, availableDirections.Count)];
        }

        return randomDirection;
    }

    public bool IsIsolatedPosition(Vector3 position)
    {
        Vector3Int tilemapPosition = UtilityFunctions.GetTilemapPosition(position);
        List<Vector2Int> freeDirections = GetFreeNeighbourDirections(new Vector2Int(tilemapPosition.x, tilemapPosition.y));
        return freeDirections.Count == 0;
    }
}
