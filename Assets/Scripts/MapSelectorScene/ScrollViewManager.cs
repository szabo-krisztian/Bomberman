using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mapPanelPrefab;

    private RectTransform _rectTransform;
    private float previousScreenHeight;


    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        previousScreenHeight = Screen.height;
        InitializeMapPanels();

        StartCoroutine(ResizeAutomaticallyIfScreenSizeChanged());
    }

    private void InitializeMapPanels()
    {
        List<string> savedMapNames = SerializationModel.GetMapNames();
        foreach (string mapName in savedMapNames)
        {
            CreateMapPanel(mapName);
        }
    }
    
    private void CreateMapPanel(string mapName)
    {
        GameObject mapPanel = Instantiate(_mapPanelPrefab, transform);
        mapPanel.name = mapName;
        mapPanel.GetComponentInChildren<TMPro.TMP_Text>().text = mapName;

        float rectSize = Screen.height / 4;
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
        tilemapData.PlayerOnePosition = new Vector3Int(1, 1, 0);
        tilemapData.PlayerTwoPosition = new Vector3Int(-1, -1, 0);
        tilemapData.MapName = mapName;
        SerializationModel.SaveMap(tilemapData);

        CreateMapPanel(mapName);
    }
}