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

    /// <summary>
    /// We use a graph search and collision detection to determine the path to the nearest player. If we are unable to do it, we return an empty list.
    /// </summary>
    /// <param name="StartingPosition">World position of the intelligent zombie.</param>
    /// <returns>The whole path containing tiles.</returns>
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

    /// <summary>
    /// We use a queue to check all the corresponding neighbours.
    /// </summary>
    /// <param name="StartingPosition"></param>
    /// <returns>Player's position or null vector.</returns>
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

    /// <summary>
    /// We use the dictionaries the determine if we have already visited a certain position or not. Then we extend all the neighbours and analyze the tilemap.
    /// </summary>
    /// <param name="queue">Queue containing the neighbours.</param>
    /// <param name="playerFound">Boolean reference so we can shut down the calculation.</param>
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

    /// <summary>
    /// Helper method for extending neighbours.
    /// </summary>
    /// <param name="neighbour">World position of the neighbour tile.</param>
    /// <param name="current">World position of the current tile.</param>
    /// <param name="queue">Queue containing neighbours.</param>
    private void ExtendNeighbour(Vector3Int neighbour, Vector3Int current, Queue<Vector3Int> queue)
    {
        _distance[neighbour] = _distance[current] + 1;
        _parent[neighbour] = current;
        queue.Enqueue(neighbour);
    }

    /// <summary>
    /// Helper method that determines if we have found the player or not.
    /// </summary>
    /// <param name="position">World position of a tile.</param>
    /// <returns>boolean</returns>
    public bool IsPlayerStandingOnPosition(Vector3 position)
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        return _collisionDetector.IsTagInColliders(colliders, "Player");
    }

    /// <summary>
    /// Helper method for initializing the dictionaries.
    /// </summary>
    /// <param name="StartingPosition">World position of our zombie.</param>
    /// <param name="queue">Queue for the neighbours.</param>
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
