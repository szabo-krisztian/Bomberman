using System.Collections;
using UnityEngine;

public class ResponsivePanel : MonoBehaviour
{
    [SerializeField]
    private RectTransform _leftPanelRectTransform;

    [SerializeField]
    private RectTransform _rightPanelRectTransform;

    private Vector2 _lastScreenSize;

    private void Start()
    {
        _lastScreenSize.x = Screen.width;
        _lastScreenSize.y = Screen.height;
        OnScreenSizeChanged();
        StartCoroutine(CheckScreenSizeChange());
    }

    private IEnumerator CheckScreenSizeChange()
    {
        while (true)
        {
            if (Screen.width != _lastScreenSize.x || Screen.height != _lastScreenSize.y)
            {
                OnScreenSizeChanged();
                _lastScreenSize.x = Screen.width;
                _lastScreenSize.y = Screen.height;
            }

            yield return null;
        }
    }

    private void OnScreenSizeChanged()
    {
        float panelWidth = ((Screen.width - Screen.height) / 2);
        _leftPanelRectTransform.anchorMax = new Vector2(panelWidth / Screen.width, 1);
        _rightPanelRectTransform.anchorMin = new Vector2((Screen.width - panelWidth) / Screen.width, 0);
    }
}