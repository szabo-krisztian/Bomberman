using UnityEngine;

public class MyGhostZombieController : MyZombieController
{
    public WalkState WalkState { get; private set; }
    public GhostState GhostState { get; private set; }
    public Vector3 PivotPoint { get; private set; }

    private IState _currentState;
    private SpriteRenderer _spriteRenderer;

    protected override void Start()
    {
        base.Start();
        WalkState = new WalkState(this);
        GhostState = new GhostState(this);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SwitchState(WalkState);
    }

    public void SwitchState(IState newState)
    {
        _currentState = newState;
        _currentState.EnterState();
    }

    public void SetPivotPoint(Vector3 position)
    {
        PivotPoint = position;
    }

    public void SetImageOpacity(float opacity)
    {
        Color color = _spriteRenderer.color;
        color.a = opacity;
        _spriteRenderer.color = color;
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
