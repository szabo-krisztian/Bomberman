using System.Collections;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private float _lastingTime = 1f;

    private void Start()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(_lastingTime);
        Destroy(gameObject);
    }
}
