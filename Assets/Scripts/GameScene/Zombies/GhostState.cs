using UnityEngine;

public class GhostState : IState
{
    private Vector3 _pivotPoint;
    private BoxCollider2D _colliderBox;

    public GhostState(MyGhostZombieController controller) : base(controller)
    {
        _colliderBox = controller.GetComponent<BoxCollider2D>();
    }

    public override void EnterState()
    {
        _colliderBox.isTrigger = true;
    }

    public override void OnCollisionStay2D(Collision2D collision, Collider2D collider)
    {
        if (collider != null && collider.CompareTag("PivotPoint") && Vector3.Distance(collider.transform.position, controller.transform.position) < 0.1f)
        {
            _colliderBox.isTrigger = false;
        }
    }

    public override void RandomTickChangeDirection()
    {
        
    }

    public void ChangePivotPoint(Vector3 position)
    {
        _pivotPoint = position;
    }
}
