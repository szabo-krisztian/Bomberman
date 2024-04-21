using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const float BOMB_LIFETIME = 3.5f;
    
    private float _bombsLifetimeCountdown;
    

    private void Start()
    {
        _bombsLifetimeCountdown = 0f;
    }

    public void BombPlacedHandler(Void data)
    {
        _bombsLifetimeCountdown += BOMB_LIFETIME;
    }

    public void PlayerDiedHandler(int playerIndex)
    {
        Debug.Log("player died: " + playerIndex);
    }

    public void Update()
    {
        if (_bombsLifetimeCountdown > 0)
        {
            _bombsLifetimeCountdown -= Time.deltaTime;
        }
    }
}
