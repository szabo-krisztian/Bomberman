using System;
using System.Collections;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D _bombCollider;

    [SerializeField]
    private GameObject _explosionPrefab;

    public int radius = 2;
    private float _detonationTime = 3f;

    private void Start()
    {
        StartCoroutine(IgniteBomb());
    }

    private IEnumerator IgniteBomb()
    {
        yield return new WaitForSeconds(_detonationTime);
        StartExplosions();
        Destroy(gameObject);
    }

    private void StartExplosions()
    {
        ExplodeInStraightLine(transform.position + Vector3.up,    Vector3.up,    radius);
        ExplodeInStraightLine(transform.position + Vector3.down,  Vector3.down,  radius);
        ExplodeInStraightLine(transform.position + Vector3.left,  Vector3.left,  radius);
        ExplodeInStraightLine(transform.position + Vector3.right, Vector3.right, radius);
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
}
