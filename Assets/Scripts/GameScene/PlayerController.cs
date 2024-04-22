using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameEvent<int> PlayerDied;
    public Vector3 FacingDirection { get; private set; }

    [SerializeField]
    private PlayerSettingsSO _settings;

    [SerializeField]
    private TilemapSO _loadedMap;

    [SerializeField]
    private GameObject _bombPrefab;

    private Rigidbody2D _rigidBody;
    private CollisionDetectionModel _collisionDetector;
    private int _bombsCount;
    private int _bombRadius;
    private int _playerIndex;
    private const int MAX_BOMB_RADIUS = 8;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collisionDetector = new CollisionDetectionModel();
        _bombsCount = 1;
        _bombRadius = 1;
        _playerIndex = GetPlayerIndex();
        _settings.InitDirectionKeys();
    }

    private int GetPlayerIndex()
    {
        return gameObject.name == "Player1" ? 1 : 2;
    }

    private void Update()
    {
        UpdateFacingDirection();
        CheckIfPlayerPlacedBomb();
    }

    private void UpdateFacingDirection()
    {
        FacingDirection = Vector2.zero;

        foreach (var pair in _settings.DirectionKeys)
        {
            if (Input.GetKey(pair.Key))
            {
                FacingDirection = pair.Value;
                return;
            }
        }
    }

    private void CheckIfPlayerPlacedBomb()
    {
        if (!Input.GetKeyDown(_settings.BombKey) || !IsPlayerAbleToPlaceBomb())
        {
            return;
        }

        Vector2 bombPosition = UtilityFunctions.GetCenterPosition(_rigidBody.position);
        PlaceBomb(bombPosition);
    }

    private void PlaceBomb(Vector2 position)
    {
        GameObject bomb = Instantiate(_bombPrefab, position, Quaternion.identity);
        BombController bombController = bomb.GetComponent<BombController>();
        bombController.SetRadius(_bombRadius);
        bombController.SetPlayerIndex(_playerIndex);
        --_bombsCount;
    }

    private bool IsPlayerAbleToPlaceBomb()
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(UtilityFunctions.GetCenterPosition(_rigidBody.position));
        return !_collisionDetector.IsTagInColliders(colliders, "Bomb") && _bombsCount > 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _rigidBody.velocity = FacingDirection * _settings.Speed;
    }

    public void BombExplodedHandler(int playerIndex)
    {
        if (playerIndex == _playerIndex)
        {
            _bombsCount++;
        }
    }

    public void Die()
    {
        PlayerDied.Raise(_playerIndex);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlusBombPowerup") && !IsPowerupPickedUp(collision.gameObject))
        {
            collision.gameObject.SendMessage("OnPickedUp");
            ++_bombsCount;
        }
        if (collision.CompareTag("BigBombPowerup") && !IsPowerupPickedUp(collision.gameObject))
        {
            collision.gameObject.SendMessage("OnPickedUp");
            _bombRadius = Mathf.Min(MAX_BOMB_RADIUS, _bombRadius + 1);
        }
    }

    private bool IsPowerupPickedUp(GameObject powerupGO)
    {
        return powerupGO.GetComponent<Powerup>().IsPickedUp;
    }
}