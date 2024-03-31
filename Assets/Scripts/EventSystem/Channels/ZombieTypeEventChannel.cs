using UnityEngine;

[CreateAssetMenu(fileName = "New ZombieType Channel", menuName = "ScriptableObjects/Events/ZombieType Event Channel")]
public class ZombieTypeEventChannel : GameEvent<ZombieType> { }

public class ZombieType
{
    public string Type { get; private set; }
    public int Count { get; private set; }

    public ZombieType(string type, int count)
    {
        Type = type;
        Count = count;
    }
}