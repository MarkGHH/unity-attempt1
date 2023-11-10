using UnityEngine;
using UnityEngine.Events;
using System.IO;

/// <summary>
/// This script handles the creation of the file. It takes input from other scripts based on the OnSaveGame and OnLoadGame events. It should not be necessary to adjust this script, all adjustments should be made in:
/// - SaveData (What data is saved, and passed to this script)
/// - Save script specific to an object (Obtaining and applying values from and to the specific object based on Save/Load game). Examples are "Chest" and "PlayerSave".
/// </summary>
public static class SaveLoad 
{
    public static UnityAction OnSaveGame;
    public static UnityAction<SaveData> OnLoadGame;

    private static string directory = "/SaveData/";
    private static string fileName = "SaveGame.txt";

    public static bool Save(SaveData data)
    {
        OnSaveGame?.Invoke();

        string dir = Application.persistentDataPath + directory;

        GUIUtility.systemCopyBuffer = dir;

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(dir + fileName, json);

        Debug.Log("Saving game");

        return true;
    }

    public static SaveData Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        SaveData data = new SaveData();

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            data = JsonUtility.FromJson<SaveData>(json);
            OnLoadGame?.Invoke(data);
        }
        else
        {
            Debug.Log("Save file does not exist.");
        }

        return data;
    }

    public static void DeleteSavedData()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;

        if (File.Exists(fullPath)) File.Delete(fullPath);
    }
}
