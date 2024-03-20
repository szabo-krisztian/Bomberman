using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BasicZombieController : MonoBehaviour
{
    private const float RAY_LENGTH = 0.05f;
    private const float COLLIDER_OFFSET_Y = 0.35f;
    private const float COLLIDER_OFFSET_X = 0.0f;

    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _directionChangeInterval = 5.0f;

    private float _directionChangeTimer = 0.0f;

    private readonly Vector2[] _directions = { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
    private Vector2 _currentDirection = Vector2.zero;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        
    }
    void Start()
    {
        _currentDirection = GetRandomDirection();
        SetToTileCenter();
    }

    void Update()
    {
        UpdateDirectionChangerTimer();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ChangeCurrentDirection();
    }

    /// <summary>
    /// Called by the Update method, simulates a timer functionality. When the elapsed time is greater than the interval,
    /// the zombie changes its direction and a new interval is calculated "randomly".
    /// </summary>
    private void UpdateDirectionChangerTimer()
    {
        _directionChangeTimer += Time.deltaTime;

        if ( _directionChangeTimer > _directionChangeInterval + Mathf.Epsilon)
        {
            ChangeCurrentDirection();
            _directionChangeTimer = 0.0f;

            _directionChangeInterval = UnityEngine.Random.Range(5.0f, 10.0f); // randomly calculating an interval each time for more random changes
        }
    }

    /// <summary>
    /// Gets a random direction for the zombie using ray casting to calculate the available directions. 
    /// </summary>
    /// <returns>Return the random direction value. If no available direction, returns a zero vector.</returns>
    private Vector2 GetRandomDirection()
    {
        List<Vector2> availableDirections = new List<Vector2>();
        Vector2 position = transform.position;

        foreach (var direction in _directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(position + direction * 0.5f, direction, RAY_LENGTH);
            bool isDirectionAvailable = hit.collider == null || (!hit.collider.CompareTag("Box") && !hit.collider.CompareTag("Wall"));

            if (isDirectionAvailable)
            {
                availableDirections.Add(direction);
            }
        }

        if (availableDirections.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableDirections.Count);
            return availableDirections[randomIndex];
        }
        else
        {
            return Vector2.zero;
        }
    }

    /// <summary>
    /// Sets the player to the center of the tile taking into account the offset of the collider
    /// </summary>
    private void SetToTileCenter()
    {
        var colliderOffset = new Vector2(COLLIDER_OFFSET_X, COLLIDER_OFFSET_Y);
        transform.position = UtilityFunctions.GetCenterPosition(transform.position) + colliderOffset;
    }

    /// <summary>
    /// Calculates the translate value of the zombie based on its current directions
    /// and performs to movement
    /// </summary>
    private void Move()
    {
        var position = _rigidBody.position;
        var translate = _currentDirection * _speed * Time.fixedDeltaTime;

        _rigidBody.MovePosition(position + translate);
    }

    /// <summary>
    /// Changes the current direction of the zombie whenever a collision happens
    /// with a destructible object. Generates different directions until the one is not 
    /// the same as the current.
    /// </summary>
    private void ChangeCurrentDirection()
    {
        SetToTileCenter();

        var newDirection = GetRandomDirection();
        while (newDirection == _currentDirection)
        {
            newDirection = GetRandomDirection();
        }

        _currentDirection = newDirection;
    }
}
