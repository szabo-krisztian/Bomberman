using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostState : IState
{
    public GhostState(MyGhostZombieController controller) : base(controller) { }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        throw new System.NotImplementedException();
    }

    public override void RandomTickChangeDirection()
    {
        throw new System.NotImplementedException();
    }
}
