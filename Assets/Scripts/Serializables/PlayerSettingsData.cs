using UnityEngine;

[System.Serializable]
public class PlayerSettingsData
{
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Bomb;

    public PlayerSettingsData(KeyCode up, KeyCode down, KeyCode left, KeyCode right, KeyCode bomb)
    {
        Up = up;
        Down = down;
        Left = left;
        Right = right;
        Bomb = bomb;
    }

    public PlayerSettingsData() { }
}
