using System.Collections;
using UnityEngine;

public class MoveWindowToCenter : MonoBehaviour
{
    [SerializeField]
    private RectTransform _windowRect;

    private float _moveSpeed = 5f;
    private Vector3 _targetPosition;
    private Vector3 _originalPos;
    private Vector2 _lastScreenSize;

    private void Start()
    {
        SetPositions();
        _lastScreenSize = GetScreenSize();
        StartCoroutine(ResizeAutomaticallyIfScreenSizeChanged());
    }

    private void OnEnable()
    {
        _windowRect.position = _originalPos;
    }

    void Update()
    {
        _windowRect.position = Vector3.Lerp(_windowRect.position, _targetPosition, _moveSpeed * Time.deltaTime);
    }

    private IEnumerator ResizeAutomaticallyIfScreenSizeChanged()
    {
        Vector2 currentScreenSize;

        while (true)
        {
            currentScreenSize = GetScreenSize();

            if (currentScreenSize != _lastScreenSize)
            {
                SetPositions();
            }

            _lastScreenSize = currentScreenSize;
            yield return null;
        }
    }

    private Vector2 GetScreenSize()
    {
       return new Vector2(Screen.width, Screen.height);
    }

    private void SetPositions()
    {
        _originalPos = new Vector3(Screen.width / 2f, Screen.height + Screen.height / 4f, 0f);
        _windowRect.position = _originalPos;
        _targetPosition = new Vector3(Screen.width / 2f, Screen.height / 2f + Screen.height / 3.5f, 0f);
    }
}
