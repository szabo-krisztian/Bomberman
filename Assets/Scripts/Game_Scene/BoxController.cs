using System;
using System.Collections;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private float _breakTime = 2f;
    private const int _powerUpSpawnChance = 69;
    private const int numOfPowerUps = 4; // TODO: Check this
    [SerializeField]
    private GameObject[] powerUps; // TODO: Fill this in editor

    public void OnExplosionHit()
    {
        StartCoroutine(Break());
    }

    private IEnumerator Break()
    {
        yield return new WaitForSeconds(_breakTime);
        SpawnPowerUp();
        Destroy(gameObject);
    }

    private void SpawnPowerUp()
    {
        throw new NotImplementedException();
        System.Random rand = new();
        if (rand.Next(0, 100) < _powerUpSpawnChance)
        {
            Instantiate(powerUps[rand.Next(0, numOfPowerUps)], transform.position, Quaternion.identity);
        }
    }
}
