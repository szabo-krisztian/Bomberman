using System.Collections;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerUps;

    [SerializeField]
    private Transform _entityGroup;

    private float _breakTime = 0.6f;
    private const int _powerUpSpawnChance = 31;
    private readonly System.Random _random = new System.Random();
    private const int _numberOfPowerUps = 2;

    /// <summary>
    /// Event handler methods that calls when a bomb explodes nearby.
    /// </summary>
    public void Die()
    {
        StartCoroutine(Break());
    }

    /// <summary>
    /// We put all entities in this Transform group for convenience. However Boxes are instantiated runtime, this is the setter method.
    /// </summary>
    /// <param name="entityGroup">Transform component of the EntityGroup.</param>
    public void SetEntityGroup(Transform entityGroup)
    {
        _entityGroup = entityGroup;
    }

    /// <summary>
    /// Coroutine for the break behaviour.
    /// </summary>
    /// <returns>Coroutine specific type.</returns>
    private IEnumerator Break()
    {
        GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(_breakTime);
        SpawnPowerUp();
        Destroy(gameObject);
    }


    /// <summary>
    /// Spawns a Powerup GameObject randomly.
    /// </summary>
    private void SpawnPowerUp()
    {
        if (_random.Next(0, 100) < _powerUpSpawnChance)
        {
            Instantiate(powerUps[_random.Next(0, _numberOfPowerUps)], transform.position, Quaternion.identity, _entityGroup);
        }
    }
}
