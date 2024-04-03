using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResponsiveMapPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _gridSquare;

    private Vector2 _lastScreenSize;
    private GridLayoutGroup _gridLayoutGroup;
    private RectTransform _rectTrans;

    private const int SIZE_OF_MAP = 15;

    private void Start()
    {
        _lastScreenSize = GetScreenSize();
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _rectTrans = GetComponent<RectTransform>();

        SetMapAnchors();
        SetGridSize(_lastScreenSize);
        PopulateGridSquares();

        StartCoroutine(ResizeAutomaticallyIfScreenSizeChanged());
    }

    private void SetMapAnchors()
    {
        float panelSize = (Screen.width - Screen.height) / 2;
        _rectTrans.anchorMin = new Vector2(panelSize / Screen.width, 0);
        _rectTrans.anchorMax = new Vector2((panelSize + Screen.height) / Screen.width, 1);
    }

    private void SetGridSize(Vector2 screenSize)
    {
        float squareSize = screenSize.y / 15;
        _gridLayoutGroup.cellSize = new Vector2(squareSize, squareSize);
    }

    private void PopulateGridSquares()
    {
        for (int i = 0; i < SIZE_OF_MAP * SIZE_OF_MAP; ++i)
        {
            Instantiate(_gridSquare, transform);
        }
    }

    private IEnumerator ResizeAutomaticallyIfScreenSizeChanged()
    {
        Vector2 currentScreenSize;
        while (true)
        {
            currentScreenSize = GetScreenSize();

            if (currentScreenSize != _lastScreenSize)
            {
                SetMapAnchors();
                SetGridSize(currentScreenSize);
                _lastScreenSize = currentScreenSize;
            }

            _lastScreenSize = currentScreenSize;
            yield return null;
        }
    }

    private Vector2 GetScreenSize()
    {
        return new Vector2(Screen.width, Screen.height);
    }
}
