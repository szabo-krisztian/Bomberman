using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public void StartButtonHitHandler(Void data)
    {
        SceneManager.LoadScene("MapSelector");
    }

    public void SettingsButtonHitHandler(Void data)
    {
        SceneManager.LoadScene("Settings");
    }

    public void ExitButtonHitHandler(Void data)
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }
}
