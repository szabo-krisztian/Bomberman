using UnityEngine;

public abstract class IState
{
    protected MyGhostZombieController controller;

    public IState(MyGhostZombieController controller)
    {
        this.controller = controller;
    }

    public abstract void EnterState();

    public abstract void OnCollisionStay2D(Collision2D collision, Collider2D collider);

    public abstract void RandomTickChangeDirection();
}
