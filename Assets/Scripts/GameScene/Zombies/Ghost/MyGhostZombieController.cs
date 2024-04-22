using UnityEngine;

public class MyGhostZombieController : MyZombieController
{
    public WalkState WalkState { get; private set; }
    public GhostState GhostState { get; private set; }
    public Vector3 PivotPoint { get; private set; }
    public Vector3 PivotDirection { get; private set; }

    private IState _currentState;
    private Animator _animator;

    protected override void Start()
    {
        base.Start();
        WalkState = new WalkState(this, "GhostZombieRun_Normal");
        GhostState = new GhostState(this, "GhostZombieRun_Ghost");
        _animator = GetComponentInChildren<Animator>();
        SwitchState(WalkState);
    }

    public void SwitchState(IState newState)
    {
        _currentState = newState;
        _animator.Play(_currentState.ANIM_NAME);
        _currentState.EnterState();
    }

    public void SetPivotPoint(Vector3 position)
    {
        PivotPoint = position;
        PivotDirection = PivotPoint - transform.position;
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        _currentState.OnCollisionStay2D(collision);
    }

    protected override void Update()
    {
        base.Update();
        _currentState.Update();
    }

    protected override void RandomTickChangeDirection()
    {
        _currentState.RandomTickChangeDirection();
    }
}
