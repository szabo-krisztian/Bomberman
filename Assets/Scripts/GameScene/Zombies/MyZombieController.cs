using System.Collections;
using UnityEngine;

public class MyZombieController : MonoBehaviour
{
    public MyZombieModel model { get; private set; }
    public Vector2 _facingDirection { get; protected set; }

    private const float SPEED = 2f;
    private const float TICK_INTERVAL_MIN = 2f;
    private const float TICK_INTERVAL_MAX = 5f;

    private Rigidbody2D _rigidBody;
    private bool _isTimeToChangeDirection;

    protected virtual void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        model = new MyZombieModel();
        ChangeDirection(Vector2.up);
        StartTicker();
    }

    protected virtual void Update()
    {
        if (_isTimeToChangeDirection && Vector2.Distance(transform.position, UtilityFunctions.GetCenterPosition(transform.position)) < .05f)
        {
            _isTimeToChangeDirection = false;
            RandomTickChangeDirection();
            StartTicker();
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        ChangeDirection(model.GetRandomDirection(transform.position, _facingDirection));
    }

    public void ChangeDirection(Vector2 facingDirection)
    {
        transform.position = UtilityFunctions.GetCenterPosition(transform.position);
        _facingDirection = facingDirection;
    }

    protected virtual void RandomTickChangeDirection()
    {
        ChangeDirection(model.GetRandomDirection(transform.position, _facingDirection));
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
        _rigidBody.MovePosition(transform.position + new Vector3(_facingDirection.x, _facingDirection.y, 0) * Time.fixedDeltaTime * SPEED);
    }
}
