using UnityEngine;

public class ExplosionDetector : MonoBehaviour
{
    private bool _isInExplosionPrefab;

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isInExplosionPrefab = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _isInExplosionPrefab = true;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (_isInExplosionPrefab)
        {
            Debug.Log("die");
        }
    }

    private void Update()
    {
        //Debug.Log(UtilityFunctions.GetTilemapPosition(transform.position));
    }
}
