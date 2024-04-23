using UnityEngine;

[CreateAssetMenu(fileName = "New_EggCracked_Channel", menuName = "ScriptableObjects/Events/EggCracked Event Channel")]
public class EggCrackedEventChannel : GameEvent<EggCrackedInfo> { }

public class EggCrackedInfo
{
    public GameObject Dino { get; private set; }
    public Vector3 Position { get; private set; }

    public EggCrackedInfo(GameObject dino, Vector3 position)
    {
        Dino = dino;
        Position = position;
    }
}