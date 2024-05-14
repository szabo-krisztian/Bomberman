using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewManager : MonoBehaviour
{
    [SerializeField]
    private GameEvent<GameObject> ExitUIPanel;

    [SerializeField]
    private GameObject _mapCreationPanel;

    [SerializeField]
    private GameObject _mapPanelPrefab;

    [SerializeField]
    private GameObject _defaultMapPanelPrefab;

    private RectTransform _rectTransform;
    private float previousScreenHeight;

    /// <summary>
    /// Built-in Unity method. We initialize the map UI windows in the scroll view.
    /// </summary>
    private void Start()
    {
        SerializationModel.InitTilemapDirectory();

        _rectTransform = GetComponent<RectTransform>();
        previousScreenHeight = Screen.height;

        InitializeDefaultMapPanels();
        InitializeMapPanels();

        StartCoroutine(ResizeAutomaticallyIfScreenSizeChanged());
    }

    /// <summary>
    /// We first create the default maps in the scrollview.
    /// </summary>
    private void InitializeDefaultMapPanels()
    {
        GameObject mapPanel1 = Instantiate(_defaultMapPanelPrefab, transform);
        mapPanel1.name = SerializationModel.DEF1_MAP_NAME;
        mapPanel1.GetComponentInChildren<TMPro.TMP_Text>().text = "def1";
        ResizeContentRect(Screen.height / 4);

        
        GameObject mapPanel2 = Instantiate(_defaultMapPanelPrefab, transform);
        mapPanel2.name = SerializationModel.DEF2_MAP_NAME;
        mapPanel2.GetComponentInChildren<TMPro.TMP_Text>().text = "def2";
        ResizeContentRect(Screen.height / 4);

        GameObject mapPanel3 = Instantiate(_defaultMapPanelPrefab, transform);
        mapPanel3.name = SerializationModel.DEF3_MAP_NAME;
        mapPanel3.GetComponentInChildren<TMPro.TMP_Text>().text = "def3";
        ResizeContentRect(Screen.height / 4);
    }

    /// <summary>
    /// Helper method that reads from the saved data and instantiates UI windows.
    /// </summary>
    private void InitializeMapPanels()
    {
        List<string> savedMapNames = SerializationModel.GetMapNames();
        foreach (string mapName in savedMapNames)
        {
            bool isNotDefaultMap = mapName != SerializationModel.DEF1_MAP_NAME && mapName != SerializationModel.DEF2_MAP_NAME && mapName != SerializationModel.DEF3_MAP_NAME;
            if (isNotDefaultMap)
            {
                CreateMapPanel(mapName);
            }
        }
    }
    
    /// <summary>
    /// Instantiates the window and sets its title, size.
    /// </summary>
    /// <param name="mapName"></param>
    private void CreateMapPanel(string mapName)
    {
        GameObject mapPanel = Instantiate(_mapPanelPrefab, transform);
        mapPanel.name = mapName;
        mapPanel.GetComponentInChildren<TMPro.TMP_Text>().text = mapName;

        ResizeContentRect(Screen.height / 4);
        ExitUIPanel.Raise(_mapCreationPanel);
    }

    private void ResizeContentRect(float rectSize)
    {
        Vector2 biggerContentSize = new Vector2(0, _rectTransform.sizeDelta.y + rectSize);
        _rectTransform.sizeDelta = biggerContentSize;
    }

    /// <summary>
    /// Coroutine that is responsible for the responsivity of the UI.
    /// </summary>
    /// <returns>Coroutine specific type.</returns>
    private IEnumerator ResizeAutomaticallyIfScreenSizeChanged()
    {
        while (true)
        {
            float currentScreenHeight = Screen.height;

            if (currentScreenHeight != previousScreenHeight)
            {
                float ratioDelta = currentScreenHeight / previousScreenHeight;
                _rectTransform.sizeDelta = new Vector2(0, _rectTransform.sizeDelta.y * ratioDelta);
                
            }

            previousScreenHeight = currentScreenHeight;
            yield return null;
        }
    }

    public void NewMapCreatedHandler(string mapName)
    {
        TilemapData tilemapData = new TilemapData();
        tilemapData.PlayerOnePosition = new Vector3Int(-7, 6, 0);
        tilemapData.PlayerTwoPosition = new Vector3Int(5, -6, 0);
        tilemapData.MapName = mapName;
        SerializationModel.SaveMap(tilemapData);

        CreateMapPanel(mapName);
    }
}