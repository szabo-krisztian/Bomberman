using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameEvent<PlayerScore> PlayerWon;

    [SerializeField]
    private TilemapInitializer _tilemapInitializer;

    private const float BOMB_LIFETIME = 3.5f;

    private bool _isPlayer1Alive;
    private bool _isPlayer2Alive;
    private int _player1Score = 0;
    private int _player2Score = 0;
    private float _bombsLifetimeCountdown;
    

    private void Start()
    {
        StartNewGame();
    }

    public void BombPlacedHandler(Void data)
    {
        _bombsLifetimeCountdown = BOMB_LIFETIME;
    }

    public void PlayerDiedHandler(int playerIndex)
    {
        if (playerIndex == 1)
        {
            _isPlayer1Alive = false;
        }
        else if (playerIndex == 2)
        {
            _isPlayer2Alive = false;
        }
    }

    private void Update()
    {
        if (_bombsLifetimeCountdown > 0)
        {
            _bombsLifetimeCountdown -= Time.deltaTime;
        }

        if (_bombsLifetimeCountdown <= 0)
        {
            CheckEndOfGame();
        }
    }

    private void CheckEndOfGame()
    {
        if (_isPlayer1Alive && _isPlayer2Alive)
        {
            return;
        }

        if (!_isPlayer1Alive && !_isPlayer2Alive)
        {
            ++_player1Score;
            PlayerWon.Raise(new PlayerScore(_player1Score, 1));

            ++_player2Score;
            PlayerWon.Raise(new PlayerScore(_player2Score, 2));
        }
        else if (!_isPlayer1Alive && _isPlayer2Alive)
        {
            Debug.Log("saf");
            ++_player2Score;
            PlayerWon.Raise(new PlayerScore(_player2Score, 2));
        }
        else if (_isPlayer1Alive && !_isPlayer2Alive)
        {
            ++_player1Score;
            PlayerWon.Raise(new PlayerScore(_player1Score, 1));
        }

        StartNewGame();
    }

    private void StartNewGame()
    {
        _isPlayer1Alive = true;
        _isPlayer2Alive = true;
        
        _bombsLifetimeCountdown = 0f;
        _tilemapInitializer.RestartGame();
    }
}
