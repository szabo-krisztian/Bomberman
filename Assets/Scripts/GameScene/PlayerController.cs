using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private PlayerSettingsSO _settings;

    [SerializeField]
    private GameObject _bombPrefab;

    [SerializeField]
    private TilemapSO _loadedMap;

    private Vector2 _facingDirection;
    private CollisionDetectionModel _collisionDetector;
    
    private void Start()
    {
        transform.position = UtilityFunctions.GetCenterPosition(_loadedMap.TilemapData.PlayerOnePosition);
        _collisionDetector = new CollisionDetectionModel();
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
        if (Input.GetKeyDown(_settings.BombKey) && IsPlayerAbleToPlaceBomb())
        {
            Vector2 bombPosition = UtilityFunctions.GetCenterPosition(_rigidBody.position);
            Instantiate(_bombPrefab, bombPosition, Quaternion.identity);
        }
    }

    private bool IsPlayerAbleToPlaceBomb()
    {
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(UtilityFunctions.GetCenterPosition(_rigidBody.position));
        return !_collisionDetector.IsTagInColliders(colliders, "Bomb");
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
}
