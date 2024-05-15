using UnityEngine;

/// <summary>
/// This is the ghost state for our ghost zombie. Now he is able to parse through walls with ease.
/// </summary>
public class GhostState : IState
{
    private BoxCollider2D _colliderBox;

    public GhostState(MyGhostZombieController controller, string animName) : base(controller, animName)
    {
        _colliderBox = controller.GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// We turn off the collider physics.
    /// </summary>
    public override void EnterState()
    {
        _colliderBox.isTrigger = true;
    }

    /// <summary>
    /// We check every frame if our zombie has passed the pivot point or not. We could have used dot vector distance but this is more optimal. Our zombies are moving in the 2 dimensional plane, in only 4 directions. If the pivot point is called A and the current position of our zombie is B then we only check if the B - A and the zombie's facingDirection vector are parallel to each other.
    /// </summary>
    public override void Update()
    {
        if (!UtilityFunctions.AreVectorsAligned(controller.PivotDirection, controller.PivotPoint - controller.transform.position))
        {
            _colliderBox.isTrigger = false;
            controller.transform.position = UtilityFunctions.GetCenterPosition(controller.transform.position);
            controller.SwitchState(controller.WalkState);
        }
    }

    /// <summary>
    /// When the zombie is in the ghost state he doesn't change directions randomly.
    /// </summary>
    public override void RandomTickChangeDirection() { }


    /// <summary>
    /// Built-in Unity method for handling collisions.
    /// </summary>
    /// <param name="collision">Collider component of a GameObject.</param>
    public override void OnCollisionStay2D(Collision2D collision) { }
}
