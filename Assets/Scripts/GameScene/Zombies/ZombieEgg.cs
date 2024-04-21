using System.Collections;
using UnityEngine;

public class ZombieEgg : MonoBehaviour
{
    public GameEvent<EggCrackedInfo> EggCracked;
    [SerializeField] private GameObject _dino;

    private const float CRACK_INTERVAL_MIN = 2f;
    private const float CRACK_INTERVAL_MAX = 5f;
    private const float CRACK_TIME = 1.55f;
    private Animator _animator;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(Random.Range(CRACK_INTERVAL_MIN, CRACK_INTERVAL_MAX));
        _animator.enabled = true;

        yield return new WaitForSeconds(CRACK_TIME);
        EggCracked.Raise(new EggCrackedInfo(_dino, transform.position));

        Destroy(gameObject);
    }
}