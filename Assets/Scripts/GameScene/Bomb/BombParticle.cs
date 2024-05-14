using System.Collections;
using UnityEngine;

/// <summary>
/// This script servers no purpose for the gameplay.
/// </summary>
public class BombParticle : MonoBehaviour
{
    private const float _particleLastingTime = 1.1f;

    private void Start()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(_particleLastingTime);
        Destroy(gameObject);
    }
}
