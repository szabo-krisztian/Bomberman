using UnityEngine;

public class AnimationRunFlip : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private MyZombieController _zombieController;
    private Vector2 _previousDirection;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _zombieController = GetComponentInParent<MyZombieController>();
        _previousDirection = _zombieController._facingDirection;
    }

    private void Update()
    {
        Vector2 currentDirection = _zombieController._facingDirection;
        if (currentDirection != _previousDirection)
        {
            FlipSprite(currentDirection);
        }
        _previousDirection = currentDirection;
    }

    private void FlipSprite(Vector2 direction)
    {
        if (direction == Vector2.left)
        {
            _spriteRenderer.flipX = true;
        }
        else if (direction == Vector2.right)
        {
            _spriteRenderer.flipX = false;
        }
    }
}
