using UnityEngine;

[CreateAssetMenu(fileName = "New MapSO", menuName = "ScriptableObjects/MapSO")]
public class TilemapSO : ScriptableObject
{
    public string MapName;
    public TilemapData TilemapData;

    public void Reset()
    {
        MapName = "__EMPTY__";
        TilemapData = new TilemapData();
    }
}