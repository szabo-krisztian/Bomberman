using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SerializationModel
{
    public static readonly string PLAYER1_SETTINGS_FILENAME = "__player1__";
    public static readonly string PLAYER2_SETTINGS_FILENAME = "__player2__";
    public static readonly string PLAYER_DIRECTORY_NAME = "PlayerSettings";

    public static readonly string DEF1_MAP_NAME = "__def1__";
    public static readonly string DEF2_MAP_NAME = "__def2__";
    public static readonly string DEF3_MAP_NAME = "__def3__";
    public static readonly string MAP_DIRECTORY_NAME = "Tilemaps";

    /// <summary>
    /// Create directories for the player settings and maps.
    /// </summary>
    public static void InitTilemapDirectory() 
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, MAP_DIRECTORY_NAME);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);

            CopyDefaultMap(DEF1_MAP_NAME);
            CopyDefaultMap(DEF2_MAP_NAME);
            CopyDefaultMap(DEF3_MAP_NAME);
        }
    }

    /// <summary>
    /// Helper method for copying maps into directories.
    /// </summary>
    /// <param name="mapName">Name of the map.</param>
    private static void CopyDefaultMap(string mapName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, mapName + ".json");
        string json = File.ReadAllText(filePath);
        SaveMap(JsonUtility.FromJson<TilemapData>(json));
    }

    /// <summary>
    /// Saved the TilemapData to a .json file.
    /// </summary>
    /// <param name="tilemapData">Converted data.</param>
    public static void SaveMap(TilemapData tilemapData)
    {
        string filePath = GetTilemapsFilePath(tilemapData.MapName + ".json");
        string json = JsonUtility.ToJson(tilemapData);
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Helper method that returns the tilemap directory path.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns>Path string.</returns>
    private static string GetTilemapsFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, "Tilemaps", fileName);
    }
    
    /// <summary>
    /// Loads the saved map into a TilemapData.
    /// </summary>
    /// <param name="fileName">Name of the map.</param>
    /// <returns>TilemapData Serializable.</returns>
    public static TilemapData LoadMap(string fileName)
    {
        string filePath = GetTilemapsFilePath(fileName + ".json");

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("File does not exist: " + filePath);
            return null;
        }

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<TilemapData>(json);
    }

    public static void DeleteMap(string fileName)
    {
        string filePath = GetTilemapsFilePath(fileName + ".json");
        File.Delete(filePath);
    }

    /// <summary>
    /// Returns all the saved map names in the tilemap directory.
    /// </summary>
    /// <returns>List of names.</returns>
    public static List<string> GetMapNames()
    {
        List<string> mapNames = new List<string>();

        string directoryPath = Path.Combine(Application.persistentDataPath, "Tilemaps");
        string[] files = Directory.GetFiles(directoryPath);
        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            mapNames.Add(fileName);
        }

        return mapNames;
    }

    public static void InitPlayerSettingsDirectory()
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, PLAYER_DIRECTORY_NAME);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);

            CopyDefaultPlayerSettings(PLAYER1_SETTINGS_FILENAME);
            CopyDefaultPlayerSettings(PLAYER2_SETTINGS_FILENAME);
        }
    }

    private static void CopyDefaultPlayerSettings(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
        string json = File.ReadAllText(filePath);
        SavePlayerSettings(JsonUtility.FromJson<PlayerSettingsData>(json), fileName);
    }  

    private static string GetPlayerSettingsFilePath(string player)
    {
        return Path.Combine(Application.persistentDataPath, "PlayerSettings", player);
    }

    public static void SavePlayerSettings(PlayerSettingsData data, string player)
    {
        string filePath = GetPlayerSettingsFilePath(player + ".json");
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }

    public static PlayerSettingsData LoadPlayerSettings(int playerIndex)
    {
        string playerName = playerIndex == 1 ? PLAYER1_SETTINGS_FILENAME : PLAYER2_SETTINGS_FILENAME;
        string filePath = GetPlayerSettingsFilePath(playerName + ".json");

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("File does not exist: " + filePath);
            return null;
        }

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<PlayerSettingsData>(json);
    }
}