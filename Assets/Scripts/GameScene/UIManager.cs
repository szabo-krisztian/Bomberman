using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _player1BombCounter;

    [SerializeField]
    private TextMeshProUGUI _player2BombCounter;

    [SerializeField]
    private TextMeshProUGUI _player1BigBombCounter;

    [SerializeField]
    private TextMeshProUGUI _player2BigBombCounter;

    [SerializeField]
    private TextMeshProUGUI _player1Wins;

    [SerializeField]
    private TextMeshProUGUI _player2Wins;

    public void BigBombPickedUpHandler(PlayerScore info)
    {
        if (info.PlayerIndex == 1)
        {
            _player1BigBombCounter.text = info.Score.ToString();
        }
        else if (info.PlayerIndex == 2)
        {
            _player2BigBombCounter.text = info.Score.ToString();
        }
    }

    public void PlusBombPickedUpHandler(PlayerScore info)
    {
        if (info.PlayerIndex == 1)
        {
            _player1BombCounter.text = info.Score.ToString();
        }
        else if (info.PlayerIndex == 2)
        {
            _player2BombCounter.text = info.Score.ToString();
        }
    }

    public void PlayerWonHandler(PlayerScore info)
    {
        if (info.PlayerIndex == 1)
        {
            _player1Wins.text = info.Score.ToString();
        }
        else if (info.PlayerIndex == 2)
        {
            _player2Wins.text = info.Score.ToString();
        }
    }
}
