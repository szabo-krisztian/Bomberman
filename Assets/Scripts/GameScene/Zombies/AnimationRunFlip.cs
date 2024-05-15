using UnityEngine;

public class AnimationRunFlip : MonoBehaviour
{
    [SerializeField]
    private string _runAnim;

    [SerializeField]
    private string _idleAnim;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private MyZombieController _zombieController;
    private Vector3 _previousDirection;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _zombieController = GetComponentInParent<MyZombieController>();
        _previousDirection = _zombieController.FacingDirection;
    }

    private void Update()
    {
        Vector3 currentDirection = _zombieController.FacingDirection;
        
        if (currentDirection != _previousDirection)
        {
            FlipSprite(currentDirection);
        }
        if (_zombieController.FacingDirection == Vector3.zero)
        {
            _animator.Play(_idleAnim);
        }
        else if (_idleAnim != "GhostIdle")
        {
            _animator.Play(_runAnim);
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
