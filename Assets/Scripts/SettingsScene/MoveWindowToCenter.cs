using System.Collections;
using UnityEngine;


public class MoveWindowToCenter : MonoBehaviour
{
    [SerializeField]
    private RectTransform _windowRect;

    [SerializeField]
    private Vector2 _originalPosRatio;

    [SerializeField]
    private Vector2 _targetPosRatio;

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
        _originalPos = new Vector3(Screen.width / 2f + Screen.width * _originalPosRatio.x, Screen.height / 2f + Screen.height * _originalPosRatio.y, 0f);
        _windowRect.position = _originalPos;
        _targetPosition = new Vector3(Screen.width / 2f + Screen.width * _targetPosRatio.x, Screen.height / 2f + Screen.height * _targetPosRatio.y, 0f);
    }
}
