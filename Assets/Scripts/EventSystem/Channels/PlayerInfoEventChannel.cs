using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerInfo Channel", menuName = "ScriptableObjects/Events/PlayerInfo Event Channel")]
public class PlayerInfoEventChannel : GameEvent<PlayerInfo> { }

public class PlayerInfo
{
    public Vector3 WorldPosition { get; private set; }
    public int PlayerIndex { get; private set; }

    public PlayerInfo(Vector3 worldPosition, int playerIndex)
    {
        WorldPosition = worldPosition;
        PlayerIndex = playerIndex;
    }
}