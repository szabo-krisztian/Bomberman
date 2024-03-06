using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private PlayerSettingsSO _settings;

    [SerializeField]
    private GameObject _bombPrefab;

    private Vector2 _facingDirection;

    private void Update()
    {
        UpdateFacingDirection();
        CheckIfPlayerPlacedBomb();
    }

    private void UpdateFacingDirection()
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

    private void CheckIfPlayerPlacedBomb()
    {
        if (Input.GetKeyDown(_settings.BombKey))
        {
            Vector2 bombPosition = UtilityFunctions.GetCenterPosition(_rigidBody.position);
            Instantiate(_bombPrefab, bombPosition, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _rigidBody.velocity = _facingDirection * _settings.Speed;
    }
}
