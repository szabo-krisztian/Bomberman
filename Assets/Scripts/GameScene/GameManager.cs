using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameEvent<PlayerScore> PlayerWon;
    public GameEvent<Void> NewGameStarted;
    public GameEvent<Void> EndOfGame;

    [SerializeField]
    private GameObject _player1VictoryUI;

    [SerializeField]
    private GameObject _player2VictoryUI;

    [SerializeField]
    private GameObject _drawUI;

    private const float BOMB_LIFETIME = 4.2f;

    private bool _isPlayer1Alive;
    private bool _isPlayer2Alive;
    private int _player1Score = 0;
    private int _player2Score = 0;
    private float _bombsLifetimeCountdown;
    private int _gameNumber = 0;
    private bool _isGameEnded = false;

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

        if (_bombsLifetimeCountdown <= 0 && !_isGameEnded)
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

        if (!_isPlayer1Alive && _isPlayer2Alive)
        {
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
        if (_gameNumber == 2)
        {
            GameFinished();
        }
        else
        {
            ++_gameNumber;
            _isPlayer1Alive = true;
            _isPlayer2Alive = true;

            _bombsLifetimeCountdown = 0f;
            NewGameStarted.Raise(new Void());
        }
    }

    private void GameFinished()
    {
        _isGameEnded = true;
        EndOfGame.Raise(new Void());
        
        Debug.Log("asf");
        if (_player1Score == _player2Score)
        {
            _drawUI.SetActive(true);
        }
        else if (_player2Score > _player1Score)
        {
            _player2VictoryUI.SetActive(true);
        }
        else
        {
            _player1VictoryUI.SetActive(true);
        }
    }
}
