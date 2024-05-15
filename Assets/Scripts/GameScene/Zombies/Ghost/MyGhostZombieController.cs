using UnityEngine;

/// <summary>
/// State machine controller script
/// </summary>
public class MyGhostZombieController : MyZombieController
{
    public WalkState WalkState { get; private set; }
    public GhostState GhostState { get; private set; }
    public Vector3 PivotPoint { get; private set; }
    public Vector3 PivotDirection { get; private set; }

    private IState _currentState;
    private Animator _animator;

    /// <summary>
    /// Vast majority of the here is just animation specific stuff. However when our zombie is instantiated we set its starting state to NormalState.
    /// </summary>
    protected override void Start()
    {
        base.Start();
        WalkState = new WalkState(this, "GhostZombieRun_Normal");
        GhostState = new GhostState(this, "GhostZombieRun_Ghost");
        _animator = GetComponentInChildren<Animator>();
        SwitchState(WalkState);
    }

    /// <summary>
    /// State transition method.
    /// </summary>
    /// <param name="newState">The new state.</param>
    public void SwitchState(IState newState)
    {
        _currentState = newState;
        _animator.Play(_currentState.ANIM_NAME);
        _currentState.EnterState();
    }

    public void SetAnim(string animName)
    {
        _animator.Play(animName);
    }

    /// <summary>
    /// We set the destination for our zombie.
    /// </summary>
    /// <param name="position">New position.</param>
    public void SetPivotPoint(Vector3 position)
    {
        PivotPoint = position;
        PivotDirection = PivotPoint - transform.position;
    }

    /// <summary>
    /// Built-in Unity method for collisions.
    /// </summary>
    /// <param name="collision">Collider component of the GameObject.</param>
    protected override void OnCollisionStay2D(Collision2D collision)
    {
        _currentState.OnCollisionStay2D(collision);
    }

    /// <summary>
    /// Built-in Unity method that calls every frame.
    /// </summary>
    protected override void Update()
    {
        base.Update();
        _currentState.Update();
    }

    /// <summary>
    /// Method that calls when a random interval tick has been invoked.
    /// </summary>
    protected override void RandomTickChangeDirection()
    {
        _currentState.RandomTickChangeDirection();
    }
}
