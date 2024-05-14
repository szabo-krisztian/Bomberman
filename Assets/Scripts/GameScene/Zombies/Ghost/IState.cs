using UnityEngine;

/// <summary>
/// States of our state machine.
/// </summary>
public abstract class IState
{
    protected MyGhostZombieController controller;
    public readonly string ANIM_NAME;

    /// <summary>
    /// Contructor for our state.
    /// </summary>
    /// <param name="controller">This is the script that controlls the transitions of our states.</param>
    /// <param name="animName">Helpful data for controlling animations in each state.</param>
    public IState(MyGhostZombieController controller, string animName)
    {
        this.controller = controller;
        ANIM_NAME = animName;
    }

    /// <summary>
    /// Method that calls if we entered a certain state.
    /// </summary>
    public abstract void EnterState();

    /// <summary>
    /// The state machine controller calls this method if our GameObject collided with anything.
    /// </summary>
    /// <param name="collision">Collider component of the GameObjects.</param>
    public abstract void OnCollisionStay2D(Collision2D collision);

    /// <summary>
    /// This is a handler method that calls if a random interval tick has been raised. This is the core for the random direction change behaviour.
    /// </summary>
    public abstract void RandomTickChangeDirection();

    /// <summary>
    /// Built-in Unity method that calls every frame.
    /// </summary>
    public abstract void Update();
}
