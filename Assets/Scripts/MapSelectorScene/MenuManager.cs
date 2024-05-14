using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameEvent<string> LoadScene;

    [SerializeField]
    private TilemapSO _mapToLoad;

    [SerializeField]
    private PlayerSettingsSO _player1Settings;

    [SerializeField]
    private PlayerSettingsSO _player2Settings;

    [SerializeField]
    private GameObject _mapRoundSelectorPopUpWindow;

    /// <summary>
    /// Event handler method. Opens a pop-up window.
    /// </summary>
    /// <param name="uiPanel">Pop-up window.</param>
    public void OpenUIPanelHandler(GameObject uiPanel)
    {
        uiPanel.SetActive(true);
    }

    /// <summary>
    /// Event handler method. Closes a pop-up window.
    /// </summary>
    /// <param name="uiPanel">Pop-up window.</param>
    public void ExitUIPanelHandler(GameObject uiPanel)
    {
        uiPanel.SetActive(false);
    }

    /// <summary>
    /// Event handler method that calls if user wants to edit a map.
    /// </summary>
    /// <param name="mapName">Name of a saved map.</param>
    public void EditMapButtonHitHandler(string mapName)
    {
        _mapToLoad.TilemapData = SerializationModel.LoadMap(mapName);
        LoadScene.Raise("MapEditor");
    }

    public void BackToMenuHandler(Void data)
    {
        LoadScene.Raise("MainMenu");
    }

    /// <summary>
    /// Event handler method that calls if user wants to play a map.
    /// </summary>
    /// <param name="mapName">Name of a saved map.</param>
    public void PlayMapButtonHitHandler(string mapName)
    {
        _mapToLoad.TilemapData = SerializationModel.LoadMap(mapName);
        _mapRoundSelectorPopUpWindow.SetActive(true);
    }
}
