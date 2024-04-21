using UnityEngine;

public class WalkState : IState
{
    private GhostModel _model;
    private const int ENTER_GHOST_STATE_CHANCE = 100;
    private readonly System.Random random = new System.Random();

    public WalkState(MyGhostZombieController controller, string animName) : base(controller, animName)
    {
        _model = new GhostModel();
    }

    public override void EnterState()
    {
        
    }

    public override void Update() { }

    public override void RandomTickChangeDirection()
    {
        controller.ChangeDirection(controller.Model.GetRandomDirection(controller.transform.position));
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        Vector3 pivotPoint = _model.GetPivotPoint(controller.FacingDirection, UtilityFunctions.GetCenterPosition(controller.transform.position));

        if (IsTimeToEnterGhostState(pivotPoint, collision.collider))
        {
            controller.SetPivotPoint(pivotPoint);
            controller.SwitchState(controller.GhostState);
        }
        else
        {
            controller.ChangeDirection(controller.Model.GetRandomDirection(controller.transform.position));
        }
    }

    private bool IsTimeToEnterGhostState(Vector3 pivotPoint, Collider2D collider)
    {
        bool zombieCanParseThroughWall = UtilityFunctions.IsPositionInMap(UtilityFunctions.GetTilemapPosition(pivotPoint)) && !controller.Model.IsIsolatedPosition(pivotPoint) && !collider.CompareTag("Bomb");
        return zombieCanParseThroughWall;
    }
}
