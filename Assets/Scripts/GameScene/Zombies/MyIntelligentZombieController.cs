using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyIntelligentZombieController : MyZombieController
{
    private MyIntelligentZombieModel _graphSearch;

    protected override void Start()
    {
        base.Start();
        _graphSearch = new MyIntelligentZombieModel();
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        ChangeDirection(GetDirectionToNearestPath());
    }

    protected Vector2Int GetDirectionToNearestPath()
    {
        Vector3Int zombieTilemapPosition = UtilityFunctions.GetTilemapPosition(transform.position);
        List<Vector2Int> routeToPlayer = _graphSearch.GetRouteToPlayer(new Vector2Int(zombieTilemapPosition.x, zombieTilemapPosition.y));
        
        if (routeToPlayer.Count == 0)
        {
            return model.GetRandomDirection(transform.position, _facingDirection);
        }

        Vector2Int firstPosition = routeToPlayer.Last();
        Vector3Int direction = new Vector3Int(firstPosition.x, firstPosition.y, 0) - UtilityFunctions.GetTilemapPosition(transform.position);
        return new Vector2Int(direction.x, direction.y);
    }
}