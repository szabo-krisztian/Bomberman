using UnityEngine;

[CreateAssetMenu(fileName = "New MapSO", menuName = "ScriptableObjects/MapSO")]
public class TilemapSO : ScriptableObject
{
    public TilemapData TilemapData;
    
    public void Reset()
    {
        TilemapData = new TilemapData();
    }
}