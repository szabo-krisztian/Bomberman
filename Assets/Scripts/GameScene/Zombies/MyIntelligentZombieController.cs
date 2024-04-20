using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyIntelligentZombieController : MyZombieController
{
    private MyIntelligentZombieModel _ai;

    protected override void Start()
    {
        base.Start();
        Vector2Int upLeftCorner = new Vector2Int(-7, 7);
        Vector2Int downRightCorner = new Vector2Int(7, -6);
        _ai = new MyIntelligentZombieModel(upLeftCorner, downRightCorner);
        StartCoroutine(CenterHit());
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        Vector3Int zombieTilemapPosition = UtilityFunctions.GetTilemapPosition(transform.position);
        var firstPos = _ai.GetRouteToPlayer(new Vector2Int(zombieTilemapPosition.x, zombieTilemapPosition.y));
        var dir = new Vector3Int(firstPos.x, firstPos.y, 0) - UtilityFunctions.GetTilemapPosition(transform.position);
        Debug.Log(dir);

        transform.position = UtilityFunctions.GetCenterPosition(transform.position);
        _facingDirection = new Vector2Int(dir.x, dir.y);
    }

    protected override void RandomTickChangeDirection()
    {
        /*
        Vector3Int zombieTilemapPosition = UtilityFunctions.GetTilemapPosition(transform.position);
        var firstPos = _ai.GetRouteToPlayer(new Vector2Int(zombieTilemapPosition.x, zombieTilemapPosition.y));
        var dir = new Vector3Int(firstPos.x, firstPos.y, 0) - UtilityFunctions.GetTilemapPosition(transform.position);
        Debug.Log(dir);

        transform.position = UtilityFunctions.GetCenterPosition(transform.position);
        _facingDirection = new Vector2Int(dir.x, dir.y);
        */
    }

    
    private IEnumerator CenterHit()
    {
        while (true)
        {
            Vector3 center = UtilityFunctions.GetCenterPosition(transform.position);
            Vector3 currentPos = transform.position;
            Vector3 toCenterDir = center - currentPos;


            if (Vector3.Dot(toCenterDir, new Vector3(_facingDirection.x, _facingDirection.y, 0)) > 0 &&
                Vector2.Distance(transform.position, UtilityFunctions.GetCenterPosition(transform.position)) < .1f)
            {
                
                Vector3Int zombieTilemapPosition = UtilityFunctions.GetTilemapPosition(transform.position);
                var firstPos = _ai.GetRouteToPlayer(new Vector2Int(zombieTilemapPosition.x, zombieTilemapPosition.y));
                var dir = new Vector3Int(firstPos.x, firstPos.y, 0) - UtilityFunctions.GetTilemapPosition(transform.position);
                Debug.Log(dir);

                transform.position = UtilityFunctions.GetCenterPosition(transform.position);
                _facingDirection = new Vector2Int(dir.x, dir.y);
            }

            if (!(Vector3.Dot(toCenterDir, new Vector3(_facingDirection.x, _facingDirection.y, 0)) > 0))
            {
                Debug.Log("facing: " + _facingDirection + ", toCenterDir: " + toCenterDir );
            }

            yield return null;
        }
    }
}