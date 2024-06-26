using UnityEngine;

/// <summary>
/// Blocker window. User cannot click on UI elements only in our pop-up window.
/// </summary>
public class BlockerWindow : MonoBehaviour
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
