using System.Collections;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
