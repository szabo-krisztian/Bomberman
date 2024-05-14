using System.Collections;
using UnityEngine;

/// <summary>
/// Parent class for all of our zombies.
/// </summary>
public class MyZombieController : MonoBehaviour
{
    public MyZombieModel Model { get; private set; }
    public Vector3 FacingDirection { get; protected set; }

    private const float SPEED = 2f;
    private const float TICK_INTERVAL_MIN = 1f;
    private const float TICK_INTERVAL_MAX = 4f;
    private const float TURNING_PRECISION = .05f;

    private Rigidbody2D _rigidBody;
    private bool _isTimeToChangeDirection;

    /// <summary>
    /// Built-in Unity method. When the GameObject is instantiated this method calls. We set the start direction for the zombie and start a Ticker Coroutine that is going to send signals for our zombie.
    /// </summary>
    protected virtual void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        Model = new MyZombieModel();
        ChangeDirection(Vector3.up);
        StartTicker();
    }

    /// <summary>
    /// Built-in Unity method that calls every frame. If the ticker has sent a signal and our zombie is close enough to a center of a tile, he changed direction. This method is mostly overriden in the subclasses.
    /// </summary>
    protected virtual void Update()
    {
        bool zombieReachedCenterOfCell = Vector3.Distance(transform.position, UtilityFunctions.GetCenterPosition(transform.position)) < TURNING_PRECISION;
        if (_isTimeToChangeDirection && zombieReachedCenterOfCell)
        {
            _isTimeToChangeDirection = false;
            RandomTickChangeDirection();
            StartTicker();
        }
    }

    /// <summary>
    /// Built-in Unity method for handling collisions.
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        ChangeDirection(Model.GetRandomDirection(transform.position));
    }

    /// <summary>
    /// We change to a random direction that is not our current direction.
    /// </summary>
    /// <param name="facingDirection">Our current direction.</param>
    public void ChangeDirection(Vector3 facingDirection)
    {
        transform.position = UtilityFunctions.GetCenterPosition(transform.position);
        FacingDirection = facingDirection;
    }

    /// <summary>
    /// Method responsibe for handling signals.
    /// </summary>
    protected virtual void RandomTickChangeDirection()
    {
        ChangeDirection(Model.GetRandomDirection(transform.position));
    }

    /// <summary>
    /// Coroutine for invoking signals.
    /// </summary>
    /// <param name="timeToWait"></param>
    /// <returns></returns>
    private IEnumerator Ticker(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        _isTimeToChangeDirection = true;
    }

    /// <summary>
    /// Helper method for restarting the ticker Coroutine. We also send a random time interval in the parameter.
    /// </summary>
    private void StartTicker()
    {
        StartCoroutine(Ticker(UnityEngine.Random.Range(TICK_INTERVAL_MIN, TICK_INTERVAL_MAX)));
    }

    /// <summary>
    /// Built-in Unity method that calls every frame. More optimalized for dealingn with physics simulations.
    /// </summary>
    private void FixedUpdate()
    {
        _rigidBody.MovePosition(transform.position + FacingDirection * Time.fixedDeltaTime * SPEED);
    }

    /// <summary>
    /// When a bomb hits a zombie he dies.
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
    }
}
