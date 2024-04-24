using UnityEngine;

public class WalkState : IState
{
    private GhostModel _model;
    private const int _ghostingChance = 80;
    private readonly System.Random _random = new System.Random();

    public WalkState(MyGhostZombieController controller, string animName) : base(controller, animName)
    {
        _model = new GhostModel();
    }

    public override void EnterState() { }

    public override void Update() { }

    public override void RandomTickChangeDirection()
    {
        controller.ChangeDirection(controller.Model.GetRandomDirection(controller.transform.position));
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        Vector3 pivotPoint = _model.GetPivotPoint(controller.FacingDirection, UtilityFunctions.GetCenterPosition(controller.transform.position));
        Vector3 position = controller.transform.position;

        if (_model.IsZombieStandingInBomb(position))
        {
            var randomFreeDirection = controller.Model.GetRandomDirection(position);
            controller.ChangeDirection(randomFreeDirection);
            controller.SetPivotPoint(controller.transform.position + randomFreeDirection);
            controller.SwitchState(controller.GhostState);
        }
        else if (IsTimeToEnterGhostState(pivotPoint, collision.collider))
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
        bool zombieCanParseThroughWall = UtilityFunctions.IsPositionInMap(UtilityFunctions.GetTilemapPosition(pivotPoint)) && !collider.CompareTag("Bomb") && _random.Next(0, 100) < _ghostingChance;
        return zombieCanParseThroughWall;
    }
}
