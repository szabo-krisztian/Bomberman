using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private float _speed;
    
    private Vector2 _facingDirection;

    private Dictionary<KeyCode, Vector2> _directions = new Dictionary<KeyCode, Vector2>
    {
        { KeyCode.A, Vector2.left },
        { KeyCode.W, Vector2.up },
        { KeyCode.S, Vector2.down },
        { KeyCode.D, Vector2.right }
    };

    private void Update()
    {
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        _facingDirection = Vector2.zero;

        foreach (var pair in _directions)
        {
            if (Input.GetKey(pair.Key))
            {
                _facingDirection = pair.Value;
                return;
            }
        }
    }

    private void FixedUpdate()
    {
        // commit test
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 position = _rigidBody.position;
        Vector2 translate = _facingDirection * _speed * Time.fixedDeltaTime;
        _rigidBody.MovePosition(position + translate);
    }
}
