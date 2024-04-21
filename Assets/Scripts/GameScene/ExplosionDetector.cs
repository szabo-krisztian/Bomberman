using UnityEngine;

public class ExplosionDetector : MonoBehaviour
{
    private CollisionDetectionModel _collisionDetector;

    public void Start()
    {
        _collisionDetector = new CollisionDetectionModel();
    }

    private void OnParticleCollision(GameObject other)
    {
        var colliders = _collisionDetector.GetCollidersInPosition(transform.position);
        if (_collisionDetector.IsTagInColliders(colliders, "Explosion"))
        {
            gameObject.SendMessage("Die");
        }
    }
}
