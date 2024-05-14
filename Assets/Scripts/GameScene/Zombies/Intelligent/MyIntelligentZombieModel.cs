using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyIntelligentZombieModel : MyZombieModel
{
    private Dictionary<Vector3Int, Vector3Int> _parent;
    private Dictionary<Vector3Int, int> _distance;

    public MyIntelligentZombieModel()
    {
        _parent = new Dictionary<Vector3Int, Vector3Int>();
        _distance = new Dictionary<Vector3Int, int>();
    }

    public List<Vector3Int> GetRouteToPlayer(Vector3Int StartingPosition)
    {
        List<Vector3Int> route = new List<Vector3Int>();

        Vector3Int playerPosition = StartPathFinding(StartingPosition);

        bool isPlayerFound = playerPosition != Vector3Int.zero;
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

    private Vector3Int StartPathFinding(Vector3Int StartingPosition)
    {
        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        InitStartingValues(StartingPosition, queue);

        Vector3Int playerFound = Vector3Int.zero;

        while (queue.Any() && playerFound == Vector3Int.zero)
        {
            ExtendNeighbours(queue, ref playerFound);
        }

        return playerFound;
    }

    private void ExtendNeighbours(Queue<Vector3Int> queue, ref Vector3Int playerFound)
    {
        Vector3Int current = queue.Dequeue();
        List<Vector3Int> neighbours = GetFreeNeighbourPositions(current);

        foreach (Vector3Int neighbour in neighbours)
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

    private void ExtendNeighbour(Vector3Int neighbour, Vector3Int current, Queue<Vector3Int> queue)
    {
        _distance[neighbour] = _distance[current] + 1;
        _parent[neighbour] = current;
        queue.Enqueue(neighbour);
    }

    public bool IsPlayerStandingOnPosition(Vector3 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return _collisionDetector.IsTagInColliders(colliders, "Player");
    }

    private void InitStartingValues(Vector3Int StartingPosition, Queue<Vector3Int> queue)
    {
        List<Vector3Int> allTilePositions = UtilityFunctions.GetAllTilePositionsInTilemap();

        foreach (Vector3Int position in allTilePositions)
        {
            _parent[new Vector3Int(position.x, position.y)] = UtilityFunctions.NULL_VECTOR;
            _distance[new Vector3Int(position.x, position.y)] = -1;
        }

        _distance[StartingPosition] = 0;
        queue.Enqueue(StartingPosition);
    }
}
