using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicZombieController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private float _speed = 5.0f;

    private Vector2 _currentDirection = Vector2.zero;
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _currentDirection = GetRandomDirection();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        MoveZombie();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Indestructibles") || collision.gameObject.CompareTag("Destructibles"))
        {
            ChangeCurrentDirection();
        }
    }

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

    /// <summary>
    /// Calculates the translate value of the zombie based on its current directions
    /// and performs to movement
    /// </summary>
    private void MoveZombie()
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
