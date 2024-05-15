using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    public GameEvent<string> LoadScene;

    public void StartButtonHitHandler(Void data)
    {
        LoadScene.Raise("MapSelector");
    }

    public void SettingsButtonHitHandler(Void data)
    {
        LoadScene.Raise("Settings");
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
