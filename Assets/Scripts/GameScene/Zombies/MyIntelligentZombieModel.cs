using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyIntelligentZombieModel : MyZombieModel
{
    private readonly Vector2Int MAP_UPPER_LEFT_CORNER;
    private readonly Vector2Int MAP_DOWN_RIGHT_CORNER;
    private readonly Vector2Int NULL_VECTOR;

    private Dictionary<Vector2Int, Vector2Int> _parent;
    private Dictionary<Vector2Int, int> _distance;

    public MyIntelligentZombieModel(Vector2Int upperLeftCorner, Vector2Int downRightCorner)
    {
        _parent = new Dictionary<Vector2Int, Vector2Int>();
        _distance = new Dictionary<Vector2Int, int>();
        
        MAP_UPPER_LEFT_CORNER = upperLeftCorner;
        MAP_DOWN_RIGHT_CORNER = downRightCorner;
        NULL_VECTOR = new Vector2Int(MAP_DOWN_RIGHT_CORNER.x + 1, MAP_DOWN_RIGHT_CORNER.y - 1);
    }

    public List<Vector2Int> GetRouteToPlayer(Vector2Int StartingPosition)
    {
        List<Vector2Int> route = new List<Vector2Int>();

        Vector2Int playerPosition = StartPathFinding(StartingPosition);

        bool isPlayerFound = playerPosition != Vector2Int.zero;
        if (isPlayerFound)
        {
            while (playerPosition != StartingPosition)
            {
                route.Add(playerPosition);
                playerPosition = _parent[playerPosition];
            }
        }

        return route;
    }

    

    private Vector2Int StartPathFinding(Vector2Int StartingPosition)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        InitStartingValues(StartingPosition, queue);

        Vector2Int playerFound = Vector2Int.zero;

        while (queue.Any() && playerFound == Vector2Int.zero)
        {
            ExtendNeighbours(queue, ref playerFound);
        }

        return playerFound;
    }

    private void ExtendNeighbours(Queue<Vector2Int> queue, ref Vector2Int playerFound)
    {
        Vector2Int current = queue.Dequeue();
        List<Vector2Int> neighbours = GetFreeNeighbourPositions(current);

        foreach (Vector2Int neighbour in neighbours)
        {
            bool isNeighbourNotExtended = _distance[neighbour] == -1;
            if (isNeighbourNotExtended)
            {
                ExtendNeighbour(neighbour, current, queue);
            }
            if (IsPlayerStandingOnPosition(UtilityFunctions.GetCenterPosition(neighbour)))
            {
                playerFound = neighbour;
                return;
            }
        }
    }

    private void ExtendNeighbour(Vector2Int neighbour, Vector2Int current, Queue<Vector2Int> queue)
    {
        _distance[neighbour] = _distance[current] + 1;
        _parent[neighbour] = current;
        queue.Enqueue(neighbour);
    }

    private bool IsPlayerStandingOnPosition(Vector2 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return _collisionDetector.IsTagInColliders(colliders, "Player");
    }

    private void InitStartingValues(Vector2Int StartingPosition, Queue<Vector2Int> queue)
    {
        for (int i = MAP_UPPER_LEFT_CORNER.x; i <= MAP_DOWN_RIGHT_CORNER.x; ++i)
        {
            for (int j = MAP_UPPER_LEFT_CORNER.y; j >= MAP_DOWN_RIGHT_CORNER.y; --j)
            {
                _parent[new Vector2Int(i, j)] = NULL_VECTOR;
                _distance[new Vector2Int(i, j)] = -1;
            }
        }

        _distance[StartingPosition] = 0;
        queue.Enqueue(StartingPosition);
    }
}
