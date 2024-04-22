using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScoreEventChannel", menuName = "ScriptableObjects/Events/PlayerScoreEventChannel")]
public class PlayerScoreEventChannel : GameEvent<PlayerScore> { }

public class PlayerScore
{
    public int Score { get; private set; }
    public int PlayerIndex { get; private set; }

    public PlayerScore(int score, int playerIndex)
    {
        Score = score;
        PlayerIndex = playerIndex;
    }
}