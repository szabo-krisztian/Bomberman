using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BasicZombieController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private float _speed = 5.0f;

    private Vector3 _currentTilemapPosition;
    private Vector3 _nextTilemapPosition;
    private Vector2 _currentDirection = Vector2.zero;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        // _currentTilemapPosition = GetCurrentTilemapPosition();
    }
    void Start()
    {
        _currentDirection = GetRandomDirection();
        SetToTileCenter();
        Debug.Log(transform.position);
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Indestructibles") || collision.gameObject.CompareTag("Destructibles"))
        {
            SetToTileCenter();
            Debug.Log("COLLISIOS");
            Debug.Log("PREV. DIR: " + _currentDirection);
            ChangeCurrentDirection();
            Debug.Log("NEW. DIR: " + _currentDirection);
        }
    }

    //private Vector3Int GetCurrentTilemapPosition()
    //{
    //      Vector3 zombieWorldPosition = transform.position;
    //    Vector3Int zombieGridPosition = _destructibles.WorldToCell(zombieWorldPosition);

    //    return zombieGridPosition;
    //}

    /// <summary>
    /// Gets a random direction for the zombies
    /// </summary>
    /// <returns>Return the random direction value</returns>
    private Vector2 GetRandomDirection()
    {
        var random = new System.Random();
        int randomIndex = random.Next(0, 4);

        switch (randomIndex)
        {
            case 0:
                return Vector2.up;
            case 1:
                return Vector2.down;
            case 2:
                return Vector2.left;
            case 3:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    private void SetToTileCenter()
    {
        Vector3 position = transform.position;
        Vector3Int flooredPosition = new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
        transform.position = flooredPosition + new Vector3(0.5f, 0.5f, 0.0f);
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
        Debug.Log("ITT VAGYOK");
        var newDirection = GetRandomDirection();
        while (newDirection == _currentDirection)
        {
            newDirection = GetRandomDirection();
        }

        _currentDirection = newDirection;
    }
}
