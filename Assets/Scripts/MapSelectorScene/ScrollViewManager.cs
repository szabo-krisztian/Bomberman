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

    private void Start()
    {
        SerializationModel.InitTilemapDirectory();
        SerializationModel.InitPlayerSettingsDirectory();

        _rectTransform = GetComponent<RectTransform>();
        previousScreenHeight = Screen.height;

        InitializeDefaultMapPanels();
        InitializeMapPanels();

        StartCoroutine(ResizeAutomaticallyIfScreenSizeChanged());
    }

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