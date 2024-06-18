using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameEvent<Void> asd;
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

    /// <summary>
    /// All player attributes are set.
    /// </summary>
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

    /// <summary>
    /// This method deals with the inputs each frame. Players can move and place bombs.
    /// </summary>
    private void Update()
    {
        UpdateFacingDirection();
        CheckIfPlayerPlacedBomb();
    }

    /// <summary>
    /// We loop through our PlayerSettings directory to check if there are any keys that have been pressed.
    /// </summary>
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

    /// <summary>
    /// We instantiate a Bomb GameObject and set its values. After the instantiation the PlusBombPickedUp event is raised.
    /// </summary>
    /// <param name="position"></param>
    private void PlaceBomb(Vector2 position)
    {
        GameObject bomb = Instantiate(_bombPrefab, position, Quaternion.identity);
        BombController bombController = bomb.GetComponent<BombController>();
        bombController.SetRadius(_bombRadius);
        bombController.SetPlayerIndex(_playerIndex);
        --_bombsCount;
        PlusBombPickedUp.Raise(new PlayerScore(_bombsCount, _playerIndex));
    }

    /// <summary>
    /// Players cannot place multiple bombs on the same tile.
    /// </summary>
    /// <returns>boolean</returns>
    private bool IsPlayerAbleToPlaceBomb()
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(UtilityFunctions.GetCenterPosition(_rigidBody.position));
        return !_collisionDetector.IsTagInColliders(colliders, "Bomb") && _bombsCount > 0;
    }

    /// <summary>
    /// Built-in Unity method that calls every frame. Optimal for physics simulations.
    /// </summary>
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _rigidBody.velocity = FacingDirection.normalized * _settings.Speed;
    }

    /// <summary>
    /// When one of the bombs have exploded, the player gets back his ammunition.
    /// </summary>
    /// <param name="playerIndex"></param>
    public void BombExplodedHandler(int playerIndex)
    {
        if (playerIndex == _playerIndex)
        {
            _bombsCount++;
            PlusBombPickedUp.Raise(new PlayerScore(_bombsCount, _playerIndex));
        }
    }

    /// <summary>
    /// Calls when the player collides with zombies or explosions.
    /// </summary>
    public void Die()
    {
        PlayerDied.Raise(_playerIndex);
        Destroy(gameObject);
    }

    /// <summary>
    /// Built-in Unity method. We use it for picking up powerups. If the player enters a collider of a GameObject this method calls.
    /// </summary>
    /// <param name="collision">We have to check if the Collider GameObject is on a Powerup GameObject or not.</param>
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