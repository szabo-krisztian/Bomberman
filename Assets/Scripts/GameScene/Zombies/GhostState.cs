using UnityEngine;

public class GhostState : IState
{
    private BoxCollider2D _colliderBox;

    public GhostState(MyGhostZombieController controller) : base(controller)
    {
        _colliderBox = controller.GetComponent<BoxCollider2D>();
    }

    public override void EnterState()
    {
        _colliderBox.isTrigger = true;
    }

    public override void Update()
    {
        if (Vector3.Distance(controller.PivotPoint, controller.transform.position) < 0.1f)
        {
            _colliderBox.isTrigger = false;
            controller.SwitchState(controller.WalkState);
        }
    }

    public override void RandomTickChangeDirection()
    {
        
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}
