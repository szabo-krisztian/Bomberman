using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameEvent<int> PlayerDied;
    public GameEvent<PlayerScore> PlusBombPickedUp;
    public GameEvent<PlayerScore> BigBombPickedUp;

    public Vector3 FacingDirection { get; private set; }

    [SerializeField]
    private PlayerSettingsSO _settings;

    [SerializeField]
    private TilemapSO _loadedMap;

    [SerializeField]
    private GameObject _bombPrefab;

    private int _playerIndex;
    private Rigidbody2D _rigidBody;
    private CollisionDetectionModel _collisionDetector;
    private int _bombsCount;
    private int _bombRadius;
    private const int MAX_BOMB_RADIUS = 8;

    private void Start()
    {
        _playerIndex = GetPlayerIndex();
        _rigidBody = GetComponent<Rigidbody2D>();
        _collisionDetector = new CollisionDetectionModel();
        _bombsCount = 1;
        _bombRadius = 1;
        _settings.InitDirectionKeys(SerializationModel.LoadPlayerSettings(_playerIndex));
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
        FacingDirection = Vector3.zero;

        foreach (var pair in _settings.DirectionKeys)
        {
            if (Input.GetKey(pair.Key))
            {
                FacingDirection += pair.Value;
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
        PlusBombPickedUp.Raise(new PlayerScore(_bombsCount, _playerIndex));
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
        _rigidBody.velocity = FacingDirection.normalized * _settings.Speed;
    }

    public void BombExplodedHandler(int playerIndex)
    {
        if (playerIndex == _playerIndex)
        {
            _bombsCount++;
            PlusBombPickedUp.Raise(new PlayerScore(_bombsCount, _playerIndex));
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
            PlusBombPickedUp.Raise(new PlayerScore(_bombsCount, _playerIndex));
        }
        if (collision.CompareTag("BigBombPowerup") && !IsPowerupPickedUp(collision.gameObject))
        {
            collision.gameObject.SendMessage("OnPickedUp");
            _bombRadius = Mathf.Min(MAX_BOMB_RADIUS, _bombRadius + 1);
            BigBombPickedUp.Raise(new PlayerScore(_bombRadius, _playerIndex));
        }
    }

    private bool IsPowerupPickedUp(GameObject powerupGO)
    {
        return powerupGO.GetComponent<Powerup>().IsPickedUp;
    }
}