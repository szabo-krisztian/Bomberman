using UnityEngine;

[CreateAssetMenu(fileName = "New TilemapBoundsSO", menuName = "ScriptableObjects/TilemapBoundsSO")]
public class TilemapBoundsSO : ScriptableObject
{
    public BoundsInt value;

    public TilemapBoundsSO(BoundsInt bounds)
    {
        value = bounds;
    }
}
