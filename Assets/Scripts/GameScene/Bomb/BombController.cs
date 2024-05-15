using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameEvent<int> BombExploded;
    public GameEvent<Void> BombPlaced;

    [SerializeField]
    private CircleCollider2D _bombCollider;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _bombParticlePrefab;
    
    private int _radius;
    private int _playerIndex;
    private float _detonationTime = 3f;
    private CollisionDetectionModel _collisionDetector;

    /// <summary>
    /// When a bomb GameObject is instantiated it immediately starts counting down and then explodes.
    /// </summary>
    private void Start()
    {
        BombPlaced.Raise(new Void());
        _collisionDetector = new CollisionDetectionModel();
        StartCoroutine(IgniteBomb(_detonationTime));
    }

    /// <summary>
    /// Coroutine that explodes the bomb after the given delay.
    /// </summary>
    /// <param name="detonationTime">Delay in seconds.</param>
    /// <returns>Coroutine specific type.</returns>
    private IEnumerator IgniteBomb(float detonationTime)
    {
        yield return new WaitForSeconds(detonationTime);
        StartExplosions();
        BombExploded.Raise(_playerIndex);
        Destroy(gameObject);
    }

    /// <summary>
    /// The bombs summons explosion GameObjects to all directions recursively.
    /// </summary>
    private void StartExplosions()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        ExplodeInStraightLine(transform.position + Vector3.up,    Vector3.up,    _radius);
        ExplodeInStraightLine(transform.position + Vector3.down,  Vector3.down,  _radius);
        ExplodeInStraightLine(transform.position + Vector3.left,  Vector3.left,  _radius);
        ExplodeInStraightLine(transform.position + Vector3.right, Vector3.right, _radius);
        SummonParticles();
    }

    /// <summary>
    /// It is just a design factor.
    /// </summary>
    private void SummonParticles()
    {
        GameObject particles = Instantiate(_bombParticlePrefab, transform.position, Quaternion.identity);
        ParticleSystem[] particleSystems = particles.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var mainModule = particleSystem.main;
            mainModule.startLifetime = 0.025f + 0.05f * _radius;
        }
    }

    /// <summary>
    /// Recursive method that implements the logic of explosions.
    /// </summary>
    /// <param name="position">World position of the current explosion.</param>
    /// <param name="direction">Facing direction of the bomb's branch.</param>
    /// <param name="length">Current length of the branch.</param>
    private void ExplodeInStraightLine(Vector2 position, Vector2 direction, int length)
    {
        if (length == 0)
        {
            return;
        }

        Instantiate(_explosionPrefab, position, Quaternion.identity);
        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        if (CheckColliders(colliders))
        {
            return;
        }

        ExplodeInStraightLine(position + direction, direction, length - 1);
    }

    /// <summary>
    /// Returns true if there is an object in the colliders array that is considered obstacle for the bomb.
    /// </summary>
    /// <param name="colliders">Collider component of the objects on a certain world position.</param>
    /// <returns>boolean</returns>
    private bool CheckColliders(Collider2D[] colliders)
    {
        return _collisionDetector.IsTagInColliders(colliders, "Box") || _collisionDetector.IsTagInColliders(colliders, "Wall");
    }

    /// <summary>
    /// Destroy the bomb after explosion.
    /// </summary>
    public void Die()
    {
        StartCoroutine(IgniteBomb(.01f));
    }

    /// <summary>
    /// Built-in Unity method that is called when a GameObject leaves the collider of the bomb.
    /// </summary>
    /// <param name="other">Collider component of the GameObject that left our collider.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        bool isPlayerExitedBomb = other.CompareTag("Player");
        if (isPlayerExitedBomb)
        {
            _bombCollider.isTrigger = false;
        }
    }

    public void SetRadius(int radius)
    {
        _radius = radius;
    }

    public void SetPlayerIndex(int playerIndex)
    {
        _playerIndex = playerIndex;
    }
}
