using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerSettingsSO _settings;

    [SerializeField]
    private TilemapSO _loadedMap;

    [SerializeField]
    private GameObject _bombPrefab;

    [SerializeField]
    private GameObject _bigBombPrefab;

    private Rigidbody2D _rigidBody;
    private CollisionDetectionModel _collisionDetector;
    private Vector2 _facingDirection;
    private int _bombsCount;
    private int _bigBombsCount;

    private void Start()
    {
        transform.position = UtilityFunctions.GetCenterPosition(_loadedMap.TilemapData.PlayerOnePosition);
        _rigidBody = GetComponent<Rigidbody2D>();
        _collisionDetector = new CollisionDetectionModel();
        _bombsCount = 5;
        _bigBombsCount = 0;
    }

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
            Debug.Log("bombsCount: " + _bombsCount + ", bigbombsCount: " + _bigBombsCount);
        }

        if (!Input.GetKeyDown(_settings.BombKey) || !IsPlayerAbleToPlaceBomb())
        {
            return;
        }

        Vector2 bombPosition = UtilityFunctions.GetCenterPosition(_rigidBody.position);
        if (_bigBombsCount > 0)
        {
            Instantiate(_bigBombPrefab, bombPosition, Quaternion.identity);
            --_bigBombsCount;
        }
        else
        {
            Instantiate(_bombPrefab, bombPosition, Quaternion.identity);
        }
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
        _rigidBody.velocity = _facingDirection * _settings.Speed;
    }

    /// <summary>
    /// Better death detection than before, but let it be inactive for testing the game
    /// </summary>
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(.05f);
        Destroy(gameObject);
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlusBombPowerup"))
        {
            collision.gameObject.SendMessage("OnPickedUp");
            ++_bombsCount;
        }
        if (collision.CompareTag("BigBombPowerup"))
        {
            collision.gameObject.SendMessage("OnPickedUp");
            ++_bigBombsCount;
        }
    }
}