using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private TilemapSO _mapToLoad;

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
}
