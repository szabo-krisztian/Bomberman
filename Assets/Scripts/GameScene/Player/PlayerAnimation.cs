using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private PlayerController _zombieController;
    private Vector3 _previousDirection;
    private Animator _animator;

    private string RUN_ANIM;
    private string IDLE_ANIM;

    private void Start()
    {
        RUN_ANIM = gameObject.name == "Player1Anim" ? "Player1Run" : "Player2Run";
        IDLE_ANIM = gameObject.name == "Player1Anim" ? "Player1Idle" : "Player2Idle";

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _zombieController = GetComponentInParent<PlayerController>();
        _previousDirection = _zombieController.FacingDirection;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 currentDirection = _zombieController.FacingDirection;
        if (currentDirection != _previousDirection)
        {
            _animator.Play(RUN_ANIM);
            FlipSprite(currentDirection);
        }
        if (currentDirection == Vector3.zero)
        {
            _animator.Play(IDLE_ANIM);
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
