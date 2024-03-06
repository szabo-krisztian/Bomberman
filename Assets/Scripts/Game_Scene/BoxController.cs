using System.Collections;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private float _breakTime = 2f;

    public void OnExplosionHit()
    {
        StartCoroutine(Break());
    }

    private IEnumerator Break()
    {
        yield return new WaitForSeconds(_breakTime);
        Destroy(gameObject);
    }
}
