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

    private void Start()
    {
        BombPlaced.Raise(new Void());
        _collisionDetector = new CollisionDetectionModel();
        StartCoroutine(IgniteBomb(_detonationTime));
    }

    private IEnumerator IgniteBomb(float detonationTime)
    {
        yield return new WaitForSeconds(detonationTime);
        StartExplosions();
        BombExploded.Raise(_playerIndex);
        Destroy(gameObject);
    }

    private void StartExplosions()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        ExplodeInStraightLine(transform.position + Vector3.up,    Vector3.up,    _radius);
        ExplodeInStraightLine(transform.position + Vector3.down,  Vector3.down,  _radius);
        ExplodeInStraightLine(transform.position + Vector3.left,  Vector3.left,  _radius);
        ExplodeInStraightLine(transform.position + Vector3.right, Vector3.right, _radius);
        SummonParticles();
    }

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

    private bool CheckColliders(Collider2D[] colliders)
    {
        return _collisionDetector.IsTagInColliders(colliders, "Box") || _collisionDetector.IsTagInColliders(colliders, "Wall");
    }

    public void Die()
    {
        StartCoroutine(IgniteBomb(.05f));
    }

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
