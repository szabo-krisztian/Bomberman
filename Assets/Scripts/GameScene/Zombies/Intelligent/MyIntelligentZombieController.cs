using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyIntelligentZombieController : MyZombieController
{
    private MyIntelligentZombieModel _graphSearch;

    /// <summary>
    /// We setup all the necessary fields that the Zombie parent class contains.
    /// </summary>
    protected override void Start()
    {
        base.Start();
        _graphSearch = new MyIntelligentZombieModel();
    }

    /// <summary>
    /// Changing the zombie's direction to the nearest path.
    /// </summary>
    /// <param name="collision">Collider component of a GameObject.</param>
    protected override void OnCollisionStay2D(Collision2D collision)
    {
        ChangeDirection(GetDirectionToNearestPath());
    }

    /// <summary>
    /// This method returns the correct direction.
    /// </summary>
    /// <returns>Direction vector.</returns>
    protected Vector3Int GetDirectionToNearestPath()
    {
        Vector3Int zombieTilemapPosition = UtilityFunctions.GetTilemapPosition(transform.position);
        List<Vector3Int> routeToPlayer = _graphSearch.GetRouteToPlayer(zombieTilemapPosition);
        
        if (routeToPlayer.Count == 0)
        {
            return Model.GetRandomDirection(transform.position);
        }

        Vector3Int firstPosition = routeToPlayer.Last();
        Vector3Int direction = firstPosition - UtilityFunctions.GetTilemapPosition(transform.position);
        return direction;
    }
}