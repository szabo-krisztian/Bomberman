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
        _currentDirection = GetRandomDirection();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private Directions GetRandomDirection()
    {
        var directionValues = Enum.GetValues(typeof(Directions));
        System.Random random = new System.Random();
        Directions randomDirection = (Directions)directionValues.GetValue(random.Next(directionValues.Length));

        return randomDirection;
    }
}
