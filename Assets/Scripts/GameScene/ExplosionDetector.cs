using UnityEngine;

public class ExplosionDetector : MonoBehaviour
{
    private bool _isInExplosionPrefab;
    private CollisionDetectionModel _collisionDetector;

    public void Start()
    {
        _collisionDetector = new CollisionDetectionModel();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isInExplosionPrefab = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("sfdf");
        _isInExplosionPrefab = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("sdf");
        var colliders = _collisionDetector.GetCollidersInPosition(transform.position);
        if (_collisionDetector.IsTagInColliders(colliders, "Explosion"))
        {
            gameObject.SendMessage("OnExplosionHit");
        }
    }
}
