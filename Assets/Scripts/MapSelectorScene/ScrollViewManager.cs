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
        InitializeMapPanels(Screen.height / 5);

        StartCoroutine(ResizeAutomaticallyIfScreenSizeChanged());
    }

    private void InitializeMapPanels(float defaultRectSize)
    {
        List<string> savedMapNames = SerializationModel.GetMapNames();
        foreach (string mapName in savedMapNames)
        {
            GameObject mapPanel = Instantiate(_mapPanelPrefab, transform);
            mapPanel.name = mapName;

            Vector2 biggerContentSize = new Vector2(0, _rectTransform.sizeDelta.y + defaultRectSize);
            _rectTransform.sizeDelta = biggerContentSize;
        }
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
}