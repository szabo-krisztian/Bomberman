using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResponsiveSmallMapPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _squarePrefab;

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
    private Coroutine showcaseCoroutine;

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
    }

    private void CleanMap()
    {
        for (int i = 0; i < MAP_SIZE; i++)
        {
            for (int j = 0; j < MAP_SIZE; j++)
            {
                if (i == 1 && j != 0 && j != MAP_SIZE - 1)
                {
                    RePrintCell(i, j, "Shadow");
                }
                else if (i != 0 && i != MAP_SIZE - 1 && j != 0 && j != MAP_SIZE - 1)
                {
                    RePrintCell(i, j, "Grass");
                }
            }
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
        else if (tileType == "Brick")
        {
            _gridLayoutGroup.transform.GetChild(index).gameObject.GetComponent<Image>().sprite = _boxSprite;
        }
        else if (tileType == "Grass")
        {
            _gridLayoutGroup.transform.GetChild(index).gameObject.GetComponent<Image>().sprite = _grassSprite;
        }
        else
        {
            _gridLayoutGroup.transform.GetChild(index).gameObject.GetComponent<Image>().sprite = _shadowGrassSprite;
        }
    }

    public void MapToInsightHandler(string mapName)
    {
        if (showcaseCoroutine != null)
        {
            StopCoroutine(showcaseCoroutine);
        }

        showcaseCoroutine = StartCoroutine(ShowcaseMap(mapName));
    }

    public IEnumerator ShowcaseMap(string mapName)
    {
        CleanMap();

        List<TileData> tiles = SerializationModel.LoadMap(mapName).Tiles;
        tiles.Shuffle();

        foreach (TileData tile in tiles)
        {
            Vector3Int converted = tile.Position - _tilemapOrigin;
            Vector2Int rowColValues = new Vector2Int((MAP_SIZE - 1) - converted.y, converted.x);
            RePrintCell(rowColValues.x, rowColValues.y, tile.TileType);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
