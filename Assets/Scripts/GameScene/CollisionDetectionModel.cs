using UnityEngine;

public class CollisionDetectionModel
{
    private float _overlapCircleRadius = .25f;

    public Collider2D[] GetCollidersInPosition(Vector3 position)
    {
        Collider2D[] overlapColliders = Physics2D.OverlapCircleAll(position, _overlapCircleRadius);
        return overlapColliders;
    }

    public bool IsTagInColliders(Collider2D[] colliders, string tag)
    {
        if (colliders == null)
        {
            return false;
        }

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
}
