using UnityEngine;

[CreateAssetMenu(fileName = "New ZombieType Channel", menuName = "ScriptableObjects/Events/ZombieType Event Channel")]
public class ZombieTypeEventChannel : GameEvent<ZombieType> { }

[System.Serializable]
public class ZombieType
{
    public string Type;
    public int Count;

    public ZombieType(string type, int count)
    {
        Type = type;
        Count = count;
    }
}