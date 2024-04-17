using UnityEngine;

public class CreateMapPanelBlockerWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject _blockerWindow;

    private void OnEnable()
    {
        _blockerWindow.SetActive(true);
    }
    private void OnDisable()
    {
        _blockerWindow.SetActive(false);
    }
}