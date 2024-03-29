using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _newMapPanel;

    [SerializeField]
    private TilemapSO _mapToLoad;

    public void NewMapButtonHitHandler()
    {
        _newMapPanel.SetActive(true);
    }

    public void NewMapBackButtonHitHandler()
    {
        _newMapPanel.SetActive(false);
    }

    public void EditMapButtonHitHandler(string mapName)
    {
        _mapToLoad.TilemapData = SerializationModel.LoadMap(mapName);
        SceneManager.LoadScene("MapEditor");
    }
}
