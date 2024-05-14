using System.Collections;
using UnityEngine;

public class ResponsiveSlider : MonoBehaviour
{
    [SerializeField]
    private RectTransform _handleRectTransform;

    private float prevHeight;

    /// <summary>
    /// We start a Coroutine that is going to change the UI elements size according to the actual screen size.
    /// </summary>
    private void Start()
    {
        prevHeight = _handleRectTransform.rect.height;
        UpdateSize(prevHeight);
        StartCoroutine(ResizeAutomaticallyIfScreenSizeChanged());
    }

    /// <summary>
    /// Coroutine that modifies the size of our UI element.
    /// </summary>
    /// <returns>Coroutine specific type.</returns>
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
