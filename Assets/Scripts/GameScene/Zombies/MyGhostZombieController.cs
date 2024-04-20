using UnityEngine;

public class MyGhostZombieController : MyZombieController
{
    [SerializeField]
    public GameObject _pivotPoint;

    public WalkState WalkState { get; private set; }
    public GhostState GhostState { get; private set; }
    private IState _currentState;

    protected override void Start()
    {
        base.Start();
        WalkState = new WalkState(this);
        GhostState = new GhostState(this);
        SwitchState(WalkState);
    }

    public void SwitchState(IState newState)
    {
        _currentState = newState;
        _currentState.EnterState();
    }

    public void SetPivotPoint(Vector3 position)
    {
        Instantiate(_pivotPoint, position, Quaternion.identity);
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        _currentState.OnCollisionStay2D(collision, null);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        _currentState.OnCollisionStay2D(null, collider);
    }

    protected override void RandomTickChangeDirection()
    {
        _currentState.RandomTickChangeDirection();
    }
}
