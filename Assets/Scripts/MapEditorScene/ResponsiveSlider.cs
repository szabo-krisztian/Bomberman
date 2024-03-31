using System.Collections;
using UnityEngine;

public class ResponsiveSlider : MonoBehaviour
{
    [SerializeField]
    private RectTransform _handleRectTransform;

    private float prevHeight;

    private void Start()
    {
        prevHeight = _handleRectTransform.rect.height;
        UpdateSize(prevHeight);
        StartCoroutine(ResizeAutomaticallyIfScreenSizeChanged());
    }



    private IEnumerator ResizeAutomaticallyIfScreenSizeChanged()
    {
        while (true)
        {
            float currentHeight = _handleRectTransform.rect.height;
            
            if (currentHeight != prevHeight)
            {
                UpdateSize(currentHeight);
            }

            prevHeight = currentHeight;
            yield return null;

        }
    }

    private void UpdateSize(float newWidth)
    {
        _handleRectTransform.sizeDelta = new Vector2(_handleRectTransform.rect.height, 0);
    }
}
