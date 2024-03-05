using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapInitializer : MonoBehaviour
{
    [SerializeField]
    private Tilemap _destructibles;

    [SerializeField]
    private Tilemap _indestructibles;

    [SerializeField]
    private TilemapSO _tilemapSO;

    [SerializeField]
    private TileBase[] _tileAsset;

    private void Start()
    {
        InitializeTilemap();
    }

    private void InitializeTilemap()
    {
        foreach (TileData tileData in _tilemapSO.TilemapData.Tiles)
        {
            Vector3Int tilePosition = tileData.Position;
            if (tileData.TileType == "Brick")
            {
                _destructibles.SetTile(tilePosition, _tileAsset[0]);
            }
            else if (tileData.TileType == "Block")
            {
                _indestructibles.SetTile(tilePosition, _tileAsset[1]);
            }
        }
    }
}
