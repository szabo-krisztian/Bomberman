using UnityEngine;

/// <summary>
/// This class represents the normal behaviour of the ghost zombie. He is standing on the grass, walking past walls.
/// </summary>
public class WalkState : IState
{
    private GhostModel _model;
    private const int _ghostingChance = 80;
    private readonly System.Random _random = new System.Random();

    public WalkState(MyGhostZombieController controller, string animName) : base(controller, animName)
    {
        _model = new GhostModel();
    }

    /// <summary>
    /// This method could be useful for dealing with special sound effects or animations. However we decided not to include these.
    /// </summary>
    public override void EnterState() { }

    /// <summary>
    /// This method could be useful for dealing with special sound effects or animations. However we decided not to include these.
    /// </summary>
    public override void Update()
    {
        if (controller.FacingDirection != Vector3.zero)
        {
            controller.SetAnim(ANIM_NAME);
        }
    }

    public override void RandomTickChangeDirection()
    {
        controller.ChangeDirection(controller.Model.GetRandomDirection(controller.transform.position));
    }

    /// <summary>
    /// Built-in Unity method. We use this for determining if the zombie needs to enter a new state or not.
    /// </summary>
    /// <param name="collision">Collider component of a GameObject.</param>
    public override void OnCollisionStay2D(Collision2D collision)
    {
        Vector3 pivotPoint = _model.GetPivotPoint(controller.FacingDirection, UtilityFunctions.GetCenterPosition(controller.transform.position));
        Vector3 position = controller.transform.position;

        if (_model.IsZombieStandingInBomb(position))
        {
            var randomFreeDirection = controller.Model.GetRandomDirection(position);
            controller.ChangeDirection(randomFreeDirection);
            controller.SetPivotPoint(controller.transform.position + randomFreeDirection);
            controller.SwitchState(controller.GhostState);
        }
        else if (IsTimeToEnterGhostState(pivotPoint, collision.collider))
        {
            controller.SetPivotPoint(pivotPoint);
            controller.SwitchState(controller.GhostState);
        }
        else
        {
            controller.ChangeDirection(controller.Model.GetRandomDirection(controller.transform.position));
        }
    }

    /// <summary>
    /// We check if the pivot point is actually correct.
    /// </summary>
    /// <param name="pivotPoint">This is the world position of a tile that servers as a destination for our ghost state.</param>
    /// <param name="collider">This is just a component of a GameObject.</param>
    /// <returns>boolean</returns>
    private bool IsTimeToEnterGhostState(Vector3 pivotPoint, Collider2D collider)
    {
        bool zombieCanParseThroughWall = UtilityFunctions.IsPositionInMap(UtilityFunctions.GetTilemapPosition(pivotPoint)) && !collider.CompareTag("Bomb") && _random.Next(0, 100) < _ghostingChance;
        return zombieCanParseThroughWall;
    }
}
