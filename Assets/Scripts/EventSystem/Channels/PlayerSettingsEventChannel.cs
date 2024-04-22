using UnityEngine;

[CreateAssetMenu(fileName = "New_PlayerSettings_Channel", menuName = "ScriptableObjects/Events/PlayerSettingsEventChannel")]
public class PlayerSettingsEventChannel : GameEvent<USER_KEY_CODE> { }

public enum USER_KEY_CODE
{
    PLAYER1_UP,
    PLAYER1_LEFT,
    PLAYER1_RIGHT,
    PLAYER1_DOWN,
    PLAYER1_BOMB,
    PLAYER2_UP,
    PLAYER2_DOWN,
    PLAYER2_LEFT,
    PLAYER2_RIGHT,
    PLAYER2_BOMB
}
