using UnityEngine;

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
