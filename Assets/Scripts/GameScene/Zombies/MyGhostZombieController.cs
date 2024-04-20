using UnityEngine;

public class MyGhostZombieController : MyZombieController
{
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

    private void SwitchState(IState newState)
    {
        _currentState = newState;
        _currentState.EnterState();
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        _currentState.OnCollisionStay2D(collision);
    }

    protected override void RandomTickChangeDirection()
    {
        _currentState.RandomTickChangeDirection();
    }
}
