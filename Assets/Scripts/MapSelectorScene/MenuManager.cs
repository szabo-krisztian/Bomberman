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

    public void PlayMapButtonHitHandler(string mapName)
    {
        _mapToLoad.TilemapData = SerializationModel.LoadMap(mapName);
        _player1Settings.InitDirectionKeys(SerializationModel.LoadPlayerSettings(SerializationModel.PLAYER1_SETTINGS_FILENAME));
        _player2Settings.InitDirectionKeys(SerializationModel.LoadPlayerSettings(SerializationModel.PLAYER2_SETTINGS_FILENAME));
        SceneManager.LoadScene("Game");
    }
}
