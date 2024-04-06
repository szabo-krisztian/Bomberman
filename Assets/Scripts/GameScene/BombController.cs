using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D _bombCollider;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _bombParticlePrefab;
    
    public int radius = 2;
    private float _detonationTime = 3f;
    private CollisionDetectionModel _collisionDetector;

    private void Start()
    {
        _collisionDetector = new CollisionDetectionModel();
        StartCoroutine(IgniteBomb(_detonationTime));
    }

    private IEnumerator IgniteBomb(float detonationTime)
    {
        yield return new WaitForSeconds(detonationTime);
        StartExplosions();
        Destroy(gameObject);
    }

    private void StartExplosions()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        ExplodeInStraightLine(transform.position + Vector3.up,    Vector3.up,    radius);
        ExplodeInStraightLine(transform.position + Vector3.down,  Vector3.down,  radius);
        ExplodeInStraightLine(transform.position + Vector3.left,  Vector3.left,  radius);
        ExplodeInStraightLine(transform.position + Vector3.right, Vector3.right, radius);
        Instantiate(_bombParticlePrefab, transform.position, Quaternion.identity);
    }

    private void ExplodeInStraightLine(Vector2 position, Vector2 direction, int length)
    {
        if (length == 0)
        {
            return;
        }

        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(position);
        if (CheckColliders(colliders))
        {
            return;
        }

        Instantiate(_explosionPrefab, position, Quaternion.identity);
        ExplodeInStraightLine(position + direction, direction, length - 1);
    }

    private bool CheckColliders(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            CheckEntityHit(collider.gameObject);
        }

        return _collisionDetector.IsTagInColliders(colliders, "Box") || _collisionDetector.IsTagInColliders(colliders, "Wall");
    }

    private void CheckEntityHit(GameObject overlappedObject)
    {
        if (overlappedObject.CompareTag("Player"))
        {
            overlappedObject.SendMessage("OnPlayerDeath");
        }
        if (overlappedObject.CompareTag("Zombie"))
        {
            overlappedObject.SendMessage("OnZombieDeath");
        }
        if (overlappedObject.CompareTag("Bomb"))
        {
            overlappedObject.SendMessage("OnBombExplodedNearby");
        }
        if (overlappedObject.CompareTag("Box"))
        {
            overlappedObject.SendMessage("OnExplosionHit");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bool isPlayerExitedBomb = other.CompareTag("Player");
        if (isPlayerExitedBomb)
        {
            _bombCollider.isTrigger = false;
        }
    }

    public void OnBombExplodedNearby()
    {
        StartCoroutine(IgniteBomb(.05f));
    }
}
