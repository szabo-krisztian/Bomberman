using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerSettings", menuName = "ScriptableObjects/PlayerSettingsSO")]
public class PlayerSettingsSO : ScriptableObject
{
    public float Speed = 5f;
    public Dictionary<KeyCode, Vector2> DirectionKeys { get; private set; }
    public KeyCode BombKey { get; private set; }
    
    public void InitDirectionKeys(PlayerSettingsData settings)
    {
        DirectionKeys = new Dictionary<KeyCode, Vector2>();
        DirectionKeys[settings.Up] = Vector3.up;
        DirectionKeys[settings.Down] = Vector3.down;
        DirectionKeys[settings.Left] = Vector3.left;
        DirectionKeys[settings.Right] = Vector3.right;
        BombKey = settings.Bomb;
    }
}
