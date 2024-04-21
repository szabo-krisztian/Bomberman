using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerSettings", menuName = "ScriptableObjects/PlayerSettingsSO")]
public class PlayerSettingsSO : ScriptableObject
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private KeyCode _bombKey = KeyCode.Space;
    [SerializeField] private KeyCode _upKey = KeyCode.W;
    [SerializeField] private KeyCode _downKey = KeyCode.S;
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;
    private Dictionary<KeyCode, Vector2> _directionKeys;

    public KeyCode BombKey { get { return _bombKey; } private set { _bombKey = value; } }
    public KeyCode UpKey { get { return _upKey; } private set { _upKey = value; } }
    public KeyCode DownKey { get { return _downKey; } private set { _downKey = value; } }
    public KeyCode LeftKey { get { return _leftKey; } private set { _leftKey = value; } }
    public KeyCode RightKey { get { return _rightKey; } private set { _rightKey = value; } }
    public float Speed { get { return _speed; } private set { _speed = value; } }
    public Dictionary<KeyCode, Vector2> DirectionKeys { get { return _directionKeys; } private set { _directionKeys = value; } }

    public void InitDirectionKeys()
    {
        _directionKeys = new Dictionary<KeyCode, Vector2>
            {
                { LeftKey, Vector2.left },
                { UpKey, Vector2.up },
                { DownKey, Vector2.down },
                { RightKey, Vector2.right }
            };
    }
}
