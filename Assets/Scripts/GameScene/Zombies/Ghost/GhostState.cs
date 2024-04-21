using UnityEngine;

public class GhostState : IState
{
    private BoxCollider2D _colliderBox;

    public GhostState(MyGhostZombieController controller, string animName) : base(controller, animName)
    {
        _colliderBox = controller.GetComponent<BoxCollider2D>();
    }

    public override void EnterState()
    {
        _colliderBox.isTrigger = true;
    }

    public override void Update()
    {
        if (Vector3.Distance(controller.PivotPoint, controller.transform.position) < 0.05f)
        {
            _colliderBox.isTrigger = false;
            controller.transform.position = UtilityFunctions.GetCenterPosition(controller.transform.position);
            controller.SwitchState(controller.WalkState);
        }
    }

    public override void RandomTickChangeDirection() { }

    public override void OnCollisionStay2D(Collision2D collision) { }
}
