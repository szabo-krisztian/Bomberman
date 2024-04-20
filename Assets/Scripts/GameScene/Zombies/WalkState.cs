using UnityEngine;

public class WalkState : IState
{
    private GhostModel _model;

    public WalkState(MyGhostZombieController controller) : base(controller)
    {
        _model = new GhostModel();
    }

    public override void EnterState()
    {
                
    }

    public override void RandomTickChangeDirection()
    {
        Debug.Log("tick");
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        Vector3 pivotPoint = _model.GetPivotPoint(controller._facingDirection, UtilityFunctions.GetCenterPosition(controller.transform.position));
        if (UtilityFunctions.IsPositionInMap(pivotPoint))
        {
            Debug.Log("enter ghost");
            if (controller.model.IsIsolatedPosition(pivotPoint))
            {
                Debug.Log("iso");
            }
        }
        else
        {
            controller.ChangeDirection(controller.model.GetRandomDirection(controller.transform.position, controller._facingDirection));
        }
    }
}
