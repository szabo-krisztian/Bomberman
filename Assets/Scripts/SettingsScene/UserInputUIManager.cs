using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInputUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _popUpWindow;

    private PlayerSettingsData _player1SettingsData;
    private PlayerSettingsData _player2SettingsData;
    private USER_KEY_CODE userKeyCode;

    private void Start()
    {
        ReadPlayerSettings();
    }

    private void Update()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode) && _popUpWindow.activeSelf)
            {
                if (IsKeyLegit(kcode))
                {
                    Modify(kcode);
                }
                
                _popUpWindow.SetActive(false);
            }
        }
    }

    public void BackToMenuHandler(Void data)
    {
        SerializationModel.SavePlayerSettings(_player1SettingsData, SerializationModel.PLAYER1_SETTINGS_FILENAME);
        SerializationModel.SavePlayerSettings(_player2SettingsData, SerializationModel.PLAYER2_SETTINGS_FILENAME);
        SceneManager.LoadScene("MainMenu");
    }

    private void ReadPlayerSettings()
    {
        _player1SettingsData = SerializationModel.LoadPlayerSettings(SerializationModel.PLAYER1_SETTINGS_FILENAME);
        _player2SettingsData = SerializationModel.LoadPlayerSettings(SerializationModel.PLAYER2_SETTINGS_FILENAME);
    }

    public void NewKeyToReadHandler(USER_KEY_CODE userKeyCode)
    {
        this.userKeyCode = userKeyCode;
        _popUpWindow.SetActive(true);
    }

    
    private bool IsKeyLegit(KeyCode key)
    {
        return _player1SettingsData.Up != key &&
               _player1SettingsData.Down != key &&
               _player1SettingsData.Left != key &&
               _player1SettingsData.Right != key &&
               _player1SettingsData.Bomb != key &&
               _player2SettingsData.Up != key &&
               _player2SettingsData.Down != key &&
               _player2SettingsData.Left != key &&
               _player2SettingsData.Right != key &&
               _player2SettingsData.Bomb != key;
        
    }

    private void Modify(KeyCode newKey)
    {
        switch (userKeyCode)
        {
            case USER_KEY_CODE.PLAYER1_UP:
                _player1SettingsData.Up = newKey;
                break;
            case USER_KEY_CODE.PLAYER1_DOWN:
                _player1SettingsData.Down = newKey;
                break;
            case USER_KEY_CODE.PLAYER1_LEFT:
                _player1SettingsData.Left = newKey;
                break;
            case USER_KEY_CODE.PLAYER1_RIGHT:
                _player1SettingsData.Right = newKey;
                break;
            case USER_KEY_CODE.PLAYER1_BOMB:
                _player1SettingsData.Bomb = newKey;
                break;
            case USER_KEY_CODE.PLAYER2_UP:
                _player2SettingsData.Up = newKey;
                break;
            case USER_KEY_CODE.PLAYER2_DOWN:
                _player2SettingsData.Down = newKey;
                break;
            case USER_KEY_CODE.PLAYER2_LEFT:
                _player2SettingsData.Left = newKey;
                break;
            case USER_KEY_CODE.PLAYER2_RIGHT:
                _player2SettingsData.Right = newKey;
                break;
            case USER_KEY_CODE.PLAYER2_BOMB:
                _player2SettingsData.Bomb = newKey;
                break;
        }
    }
}
