using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SerializationModel
{
    public static void InitTilemapDirectory(TilemapSO defaultMap)
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, "Tilemaps");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        SaveMap(defaultMap);
    }

    private static string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, "Tilemaps", fileName);
    }

    public static void SaveMap(TilemapSO tilemapSO)
    {
        string filePath = GetFilePath(tilemapSO.MapName);
        string json = JsonUtility.ToJson(tilemapSO.TilemapData);
        File.WriteAllText(filePath, json);
    }

    public static TilemapData LoadMap(string fileName)
    {
        string filePath = GetFilePath(fileName);

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
        string filePath = GetFilePath(fileName);
        File.Delete(filePath);
    }

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
}
