using System.Collections;
using UnityEngine;

/// <summary>
/// Script responsible for better-looking animation.
/// </summary>
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
