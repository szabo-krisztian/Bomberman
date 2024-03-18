using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BasicZombieController : MonoBehaviour
{
    private const float RAY_LENGTH = 0.05f;
    private const float COLLIDER_OFFSET = 0.35f;

    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private float _speed = 5.0f;

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
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        SetToTileCenter();
        ChangeCurrentDirection();
    }

    /// <summary>
    /// Gets a random direction for the zombie using ray casting to calculate the available directions 
    /// </summary>
    /// <returns>Return the random direction value. If no available direction, returns a zero vector</returns>
    private Vector2 GetRandomDirection()
    {
        List<Vector2> availableDirections = new List<Vector2>();
        Vector2 position = transform.position;

        foreach (var direction in _directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(position + direction * 0.5f, direction, RAY_LENGTH);
            bool isWallOrBoxHit = hit.collider == null || (!hit.collider.CompareTag("Box") && !hit.collider.CompareTag("Wall"));

            if (isWallOrBoxHit)
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
        Vector3 position = transform.position;
        Vector3Int flooredPosition = new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
        transform.position = flooredPosition + new Vector3(0.5f, 0.5f + COLLIDER_OFFSET, 0.0f);
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
        var newDirection = GetRandomDirection();
        while (newDirection == _currentDirection)
        {
            newDirection = GetRandomDirection();
        }

        _currentDirection = newDirection;
    }
}
