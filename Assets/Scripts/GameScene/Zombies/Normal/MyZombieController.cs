using System.Collections;
using UnityEngine;

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

    protected virtual void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        Model = new MyZombieModel();
        ChangeDirection(Vector3.up);
        StartTicker();
    }

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

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        ChangeDirection(Model.GetRandomDirection(transform.position));
    }

    public void ChangeDirection(Vector3 facingDirection)
    {
        transform.position = UtilityFunctions.GetCenterPosition(transform.position);
        FacingDirection = facingDirection;
    }

    protected virtual void RandomTickChangeDirection()
    {
        ChangeDirection(Model.GetRandomDirection(transform.position));
    }

    private IEnumerator Ticker(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        _isTimeToChangeDirection = true;
    }

    private void StartTicker()
    {
        StartCoroutine(Ticker(UnityEngine.Random.Range(TICK_INTERVAL_MIN, TICK_INTERVAL_MAX)));
    }

    private void FixedUpdate()
    {
        _rigidBody.MovePosition(transform.position + new Vector3(FacingDirection.x, FacingDirection.y, 0) * Time.fixedDeltaTime * SPEED);
    }
}
