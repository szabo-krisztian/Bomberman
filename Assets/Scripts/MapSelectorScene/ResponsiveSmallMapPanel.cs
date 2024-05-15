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

    /// <summary>
    /// Built-in Unity method. We create a UI gridmap replica from a saved map.
    /// </summary>
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

    /// <summary>
    /// Each grid in the gridmap UI will represent a tile in our saved map.
    /// </summary>
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

    /// <summary>
    /// We clean the gridman UI element.
    /// </summary>
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

    /// <summary>
    /// Coroutine that is responsible for the responsivity of the UI.
    /// </summary>
    /// <returns>Coroutine specific type.</returns>
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

    /// <summary>
    /// Helper method that sets the sprite of one grid in the UI element.
    /// </summary>
    /// <param name="row">Row index.</param>
    /// <param name="column">Column index.</param>
    /// <param name="tileType">Type of a tile.</param>
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

    /// <summary>
    /// Event handler that calls if the user has pressed the '?' button. We paint the tilemap.
    /// </summary>
    /// <param name="mapName">Name of the saved map.</param>
    public void MapToInsightHandler(string mapName)
    {
        if (showcaseCoroutine != null)
        {
            StopCoroutine(showcaseCoroutine);
        }

        showcaseCoroutine = StartCoroutine(ShowcaseMap(mapName));
    }

    /// <summary>
    /// Coroutine that is responsible for creating the 'random mess' effect during the map painting.
    /// </summary>
    /// <param name="mapName">Name of the saved map.</param>
    /// <returns>Coroutine specific type.</returns>
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

            yield return new WaitForSeconds(.01f);
        }
    }
}