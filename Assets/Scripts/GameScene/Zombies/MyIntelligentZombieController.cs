using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyIntelligentZombieController : MyZombieController
{
    private MyIntelligentZombieModel _graphSearch;

    protected override void Start()
    {
        base.Start();
        Vector2Int upLeftCorner = new Vector2Int(-7, 7);
        Vector2Int downRightCorner = new Vector2Int(7, -6);
        _graphSearch = new MyIntelligentZombieModel(upLeftCorner, downRightCorner);
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        _facingDirection = GetDirectionToNearestPath();   
    }

    protected override void RandomTickChangeDirection()
    {
        _facingDirection = GetDirectionToNearestPath();
    }

    private Vector2Int GetDirectionToNearestPath()
    {
        Vector3Int zombieTilemapPosition = UtilityFunctions.GetTilemapPosition(transform.position);
        Vector2Int firstPos = _graphSearch.GetRouteToPlayer(new Vector2Int(zombieTilemapPosition.x, zombieTilemapPosition.y)).Last();
        Vector3Int direction = new Vector3Int(firstPos.x, firstPos.y, 0) - UtilityFunctions.GetTilemapPosition(transform.position);
        transform.position = UtilityFunctions.GetCenterPosition(transform.position);
        var a = new Vector2Int(direction.x, direction.y);
        Debug.Log(a);
        return a;
    }
}