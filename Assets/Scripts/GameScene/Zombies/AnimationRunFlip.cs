using UnityEngine;

public class AnimationRunFlip : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private MyZombieController _zombieController;
    private Vector3 _previousDirection;
    private Animator _animator;

    [SerializeField]
    private string _runAnim;

    [SerializeField]
    private string _idleAnim;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _zombieController = GetComponentInParent<MyZombieController>();
        _previousDirection = _zombieController.FacingDirection;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 currentDirection = _zombieController.FacingDirection;
        
        if (currentDirection == Vector3.zero)
        {

        }
        else
        {

        }
        
        if (currentDirection != _previousDirection)
        {
            FlipSprite(currentDirection);
        }
        _previousDirection = currentDirection;
    }

    private void FlipSprite(Vector3 direction)
    {
        if (direction == Vector3.left)
        {
            _spriteRenderer.flipX = true;
        }
        else if (direction == Vector3.right)
        {
            _spriteRenderer.flipX = false;
        }
    }
}
