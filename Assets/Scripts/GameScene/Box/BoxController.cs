using System.Collections;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerUps;

    [SerializeField]
    private Transform _entityGroup;

    private float _breakTime = 0.6f;
    private const int _powerUpSpawnChance = 100;
    private readonly System.Random random = new System.Random();
    private const int _numberOfPowerUps = 2;

    public void Die()
    {
        StartCoroutine(Break());
    }

    public void SetEntityGroup(Transform entityGroup)
    {
        _entityGroup = entityGroup;
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
            Instantiate(powerUps[random.Next(0, _numberOfPowerUps)], transform.position, Quaternion.identity, _entityGroup);
        }
    }
}
