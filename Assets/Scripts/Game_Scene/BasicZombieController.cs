using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicZombieController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private float _speed;

    private Directions _currentDirection;
    void Start()
    {
        _speed = 2.0f;
        _rigidBody = GetComponent<Rigidbody2D>();
        _currentDirection = GetRandomDirection();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 position = _rigidBody.position;
        Vector2 translate = Vector2.zero;

        // calculate translate value based on the current direction
        switch (_currentDirection)
        {
            case Directions.UP:
                translate = Vector2.up * _speed * Time.fixedDeltaTime;
                break;
            case Directions.DOWN:
                translate = Vector2.down * _speed * Time.fixedDeltaTime;
                break;
            case Directions.LEFT:
                translate = Vector2.left * _speed * Time.fixedDeltaTime;
                break;
            case Directions.RIGHT:
                translate = Vector2.right * _speed * Time.fixedDeltaTime;
                break;
        }

        _rigidBody.MovePosition(position + translate);
    }

    /// <summary>
    /// Gets a random direction for the zombies
    /// </summary>
    /// <returns>Return the random direction value</returns>
    private Directions GetRandomDirection()
    {
        var directionValues = Enum.GetValues(typeof(Directions));
        System.Random random = new System.Random();
        Directions randomDirection = (Directions)directionValues.GetValue(random.Next(directionValues.Length));

        return randomDirection;
    }
}
