using UnityEngine;

public class WalkState : IState
{
    private GhostModel _model;
    private const int ENTER_GHOST_STATE_CHANCE = 80;
    private readonly System.Random random = new System.Random();

    public WalkState(MyGhostZombieController controller) : base(controller)
    {
        _model = new GhostModel();
    }

    public override void EnterState()
    {
        controller.SetImageOpacity(1f);
    }

    public override void Update()
    {
        
    }

    public override void RandomTickChangeDirection()
    {
        controller.ChangeDirection(controller.model.GetRandomDirection(controller.transform.position, controller._facingDirection));
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        Vector3 pivotPoint = _model.GetPivotPoint(controller._facingDirection, UtilityFunctions.GetCenterPosition(controller.transform.position));
        if (IsTimeToEnterGhostState(pivotPoint, collision.collider))
        {
            controller.SetPivotPoint(pivotPoint);
            controller.SwitchState(controller.GhostState);
        }
        else
        {
            controller.ChangeDirection(controller.model.GetRandomDirection(controller.transform.position, controller._facingDirection));
        }
    }

    private bool IsTimeToEnterGhostState(Vector3 pivotPoint, Collider2D collider)
    {
        bool zombieCanParseThroughWall = UtilityFunctions.IsPositionInMap(pivotPoint) && !controller.model.IsIsolatedPosition(pivotPoint) && !collider.CompareTag("Bomb");
        return zombieCanParseThroughWall && random.Next(0, 100) < ENTER_GHOST_STATE_CHANCE;
    }
}
