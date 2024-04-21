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

    protected bool IsPositionFree(Vector3 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return !_collisionDetector.IsTagInColliders(colliders, "Wall") && !_collisionDetector.IsTagInColliders(colliders, "Box") && !_collisionDetector.IsTagInColliders(colliders, "Bomb");
    }

    public List<Vector3Int> GetFreeNeighbourPositions(Vector3Int position)
    {
        List<Vector3Int> freeNeighbours = new List<Vector3Int>();

        foreach (Vector3 direction in UtilityFunctions.Directions)
        {
            Vector3 neighbourWorldPosition = UtilityFunctions.GetCenterPosition(position) + direction;
            if (IsPositionFree(neighbourWorldPosition))
            {
                freeNeighbours.Add(UtilityFunctions.GetTilemapPosition(neighbourWorldPosition));
            }
        }

        return freeNeighbours;
    }

    public List<Vector3Int> GetFreeNeighbourDirections(Vector3Int position)
    {
        List<Vector3Int> freeDirections = new List<Vector3Int>();

        foreach (Vector3Int freeNeighbourPosition in GetFreeNeighbourPositions(position))
        {
            freeDirections.Add(freeNeighbourPosition - position);
        }

        return freeDirections;
    }

    public Vector3Int GetRandomDirection(Vector3 position)
    {
        List<Vector3Int> availableDirections = GetFreeNeighbourDirections(UtilityFunctions.GetTilemapPosition(position));
        if (availableDirections.Count == 0)
        {
            return Vector3Int.zero;
        }

        Vector3Int randomDirection = availableDirections[random.Next(0, availableDirections.Count)];
        return randomDirection;
    }

    public bool IsIsolatedPosition(Vector3 position)
    {
        Vector3Int tilemapPosition = UtilityFunctions.GetTilemapPosition(position);
        List<Vector3Int> freeDirections = GetFreeNeighbourDirections(new Vector3Int(tilemapPosition.x, tilemapPosition.y));
        return freeDirections.Count == 0;
    }
}
