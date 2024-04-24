using UnityEngine;

public abstract class IState
{
    protected MyGhostZombieController controller;
    public readonly string ANIM_NAME;

    public IState(MyGhostZombieController controller, string animName)
    {
        this.controller = controller;
        ANIM_NAME = animName;
    }

    public abstract void EnterState();

    public abstract void OnCollisionStay2D(Collision2D collision);

    public abstract void RandomTickChangeDirection();

    public abstract void Update();
}
