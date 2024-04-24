using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private TilemapSO _mapToLoad;

    [SerializeField]
    private PlayerSettingsSO _player1Settings;

    [SerializeField]
    private PlayerSettingsSO _player2Settings;

    public void OpenUIPanelHandler(GameObject uiPanel)
    {
        uiPanel.SetActive(true);
    }

    public void ExitUIPanelHandler(GameObject uiPanel)
    {
        uiPanel.SetActive(false);
    }

    public void EditMapButtonHitHandler(string mapName)
    {
        _mapToLoad.TilemapData = SerializationModel.LoadMap(mapName);
        SceneManager.LoadScene("MapEditor");
    }

    public void BackToMenuHandler(Void data)
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayMapButtonHitHandler(string mapName)
    {
        _mapToLoad.TilemapData = SerializationModel.LoadMap(mapName);
        SceneManager.LoadScene("Game");
    }
}
