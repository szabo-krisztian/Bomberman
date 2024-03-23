using UnityEngine;

[CreateAssetMenu(fileName = "New MapSO", menuName = "ScriptableObjects/MapSO")]
public class TilemapSO : ScriptableObject
{
    public string MapName;
    public TilemapData TilemapData;
    
    public Vector3Int PlayerOnePosition;
    public Vector3Int PlayerTwoPosition;

    public void Reset()
    {
        MapName = "__EMPTY__";
        TilemapData = new TilemapData();
    }

    public void SetPlayerOutsideMap(int playerIndex)
    {
        if (playerIndex == 1)
        {
            PlayerOnePosition = new Vector3Int(-69, -69, -69);
        }
        else
        {
            PlayerTwoPosition = new Vector3Int(-69, -69, -69);
        }
    }
}