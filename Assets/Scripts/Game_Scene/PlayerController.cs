using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private PlayerSettingsSO _settings;

    private Vector2 _facingDirection;

    private void Update()
    {
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        _facingDirection = Vector2.zero;

        foreach (var pair in _settings.DirectionKeys)
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
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 position = _rigidBody.position;
        Vector2 translate = _facingDirection * _settings.Speed * Time.fixedDeltaTime;
        _rigidBody.MovePosition(position + translate);
    }
}
