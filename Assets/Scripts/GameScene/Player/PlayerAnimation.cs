using UnityEngine;

/// <summary>
/// This script servers no purpose for the gameplay.
/// </summary>
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

    /// <summary>
    /// Flips the sprite of a player corresponding to its facing direction.
    /// </summary>
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
        if (direction == Vector3.left || direction == Vector3.left + Vector3.up || direction == Vector3.left + Vector3.down)
        {
            _spriteRenderer.flipX = true;
        }
        else if (direction == Vector3.right || direction == Vector3.right + Vector3.up || direction == Vector3.right + Vector3.down)
        {
            _spriteRenderer.flipX = false;
        }
    }
}
