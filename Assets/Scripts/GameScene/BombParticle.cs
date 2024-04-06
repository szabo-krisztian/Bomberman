using System.Collections;
using UnityEngine;

public class BombParticle : MonoBehaviour
{
    private const float _particleLastingTime = 1f;

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
