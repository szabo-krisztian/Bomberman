using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerSettings", menuName = "ScriptableObjects/PlayerSettingsNew")]
public class PlayerSettingsSO_NEW : ScriptableObject
{
    public PlayerSettingsData Setings { get; private set; }
}
