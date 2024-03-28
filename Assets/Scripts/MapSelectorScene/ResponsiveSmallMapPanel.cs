using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ResponsiveSmallMapPanel : MonoBehaviour
{
    [SerializeField]
    private Sprite _grassSprite;

    [SerializeField]
    private Sprite _shadowGrassSprite;

    [SerializeField]
    private Sprite _wallSprite;

    [SerializeField]
    private Sprite _boxSprite;


    private RectTransform _rectTransform;
    private GridLayoutGroup _gridLayoutGroup;
    private float _lastRectSize;

    private Vector3Int _tilemapOrigin = new Vector3Int(-8, -7, 0);

    [SerializeField]
    private GameObject _squarePrefab;

    private const int MAP_SIZE = 15;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _lastRectSize = _rectTransform.rect.width;

        SetCellSize(_lastRectSize / MAP_SIZE);
        PopulateGridSquares();
        StartCoroutine(ResizeAutomaticallyIfScreenSizeChanged());
    }

    private void SetCellSize(float squareSize)
    {
        _gridLayoutGroup.cellSize = new Vector2(squareSize, squareSize);
    }

    private void PopulateGridSquares()
    {
        for (int i = 0; i < MAP_SIZE; i++)
        {
            for (int j = 0; j < MAP_SIZE; j++)
            {
                GameObject square = Instantiate(_squarePrefab, transform);
                if (i == 0 || i == MAP_SIZE - 1 || j == 0 || j == MAP_SIZE - 1)
                {
                    square.GetComponent<Image>().sprite = _wallSprite;
                }
                else if (i == 1 && j != 0 && j != MAP_SIZE - 1)
                {
                    square.GetComponent<Image>().sprite = _shadowGrassSprite;
                }
                else
                {
                    square.GetComponent<Image>().sprite = _grassSprite;
                }
            }
        }


        TilemapData tilemapData = SerializationModel.LoadMap("MASTER");
        foreach (TileData tile in tilemapData.Tiles)
        {
            Vector3Int converted = tile.Position - _tilemapOrigin;
            Vector2Int rowColValues = new Vector2Int((MAP_SIZE - 1) - converted.y, converted.x);
            RePrintCell(rowColValues.x, rowColValues.y, tile.TileType);
        }
    }

    private IEnumerator ResizeAutomaticallyIfScreenSizeChanged()
    {
        float currentRectSize;
        while (true)
        {
            currentRectSize = _rectTransform.rect.width;

            if (currentRectSize != _lastRectSize)
            {
                float newSquareSize = currentRectSize / 15;
                SetCellSize(newSquareSize);
            }

            _lastRectSize = currentRectSize;
            yield return null;
        }
    }

    public void RePrintCell(int row, int column, string tileType)
    {
        int index = row * MAP_SIZE + column;

        if (tileType == "Block")
        {
            _gridLayoutGroup.transform.GetChild(index).gameObject.GetComponent<Image>().sprite = _wallSprite;
        }
        else
        {
            _gridLayoutGroup.transform.GetChild(index).gameObject.GetComponent<Image>().sprite = _boxSprite;
        }
    }
}
