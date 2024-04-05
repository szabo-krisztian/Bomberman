using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

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

    private void Start()
    {
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

        Collider2D overlapColliderCircle = Physics2D.OverlapCircle(position, .25f);
        if (overlapColliderCircle != null)
        {
            EntityHit(overlapColliderCircle.gameObject);

            if (CheckIfBoxHit(overlapColliderCircle.gameObject) || CheckIfWallHit(overlapColliderCircle.gameObject))
            { 
                return;
            }
        }

        Instantiate(_explosionPrefab, position, Quaternion.identity);
        ExplodeInStraightLine(position + direction, direction, length - 1);
    }

    private bool CheckIfWallHit(GameObject overlappedObject)
    {
        return overlappedObject.CompareTag("Wall");
    }

    private void EntityHit(GameObject overlappedObject)
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
    }

    private bool CheckIfBoxHit(GameObject overlappedObject)
    {
        if (overlappedObject.CompareTag("Box"))
        {
            overlappedObject.SendMessage("OnExplosionHit");
            return true;
        }
        return false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bool isPlayerExitedBomb = other.CompareTag("Player");
        if (isPlayerExitedBomb)
        {
            _bombCollider.isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Explosion"))
        {
            StartExplosions();
        }
    }

    public void OnBombExplodedNearby()
    {
        StartCoroutine(IgniteBomb(0));
    }
}
