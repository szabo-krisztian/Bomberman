using System.Collections;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerUps;

    private float _breakTime = 1.1f;
    private const int _powerUpSpawnChance = 69;
    private readonly System.Random random = new System.Random();
    private const int _numberOfPowerUps = 2;

    public void OnExplosionHit()
    {
        StartCoroutine(Break());
    }

    private IEnumerator Break()
    {
        GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(_breakTime);
        SpawnPowerUp();
        Destroy(gameObject);
    }

    private void SpawnPowerUp()
    {
        if (random.Next(0, 100) < _powerUpSpawnChance)
        {
            Instantiate(powerUps[random.Next(0, _numberOfPowerUps)], transform.position, Quaternion.identity);
        }
    }
}
