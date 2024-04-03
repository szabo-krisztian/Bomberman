using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerSettings", menuName = "ScriptableObjects/PlayerSettingsSO")]
public class PlayerSettingsSO : ScriptableObject
{
    public Dictionary<KeyCode, Vector2> DirectionKeys { get; private set; } = new Dictionary<KeyCode, Vector2>
    {
        { KeyCode.A, Vector2.left },
        { KeyCode.W, Vector2.up },
        { KeyCode.S, Vector2.down },
        { KeyCode.D, Vector2.right }
    };

    public KeyCode BombKey { get; private set; } = KeyCode.B;
    public float Speed { get; private set; } = 5f;
}
