using System.Collections;
using UnityEngine;

public class ZombieEgg : MonoBehaviour
{
    private const float _crackTime = 1.75f;

    private void Start()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(_crackTime);
        Destroy(gameObject);
    }
}