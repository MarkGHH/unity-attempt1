using UnityEngine;
using System.IO;
using UnityEngine.Events;

public static class SaveGameManager
{
    public static SaveData currentSaveData = new SaveData();
    public const string SaveDirectory = "/SaveData/";
    public const string FileName = "SaveGame.txt";

    public static UnityEvent OnSaveGameStart;
    public static UnityEvent OnSaveGameFinish;
    public static UnityEvent OnLoadGameStart;
    public static UnityEvent OnLoadGameFinish;

    public static bool SaveGame()
    {
        OnSaveGameStart?.Invoke();
        var dir = Application.persistentDataPath + SaveDirectory;
        if (!Directory.Exists (dir)) Directory.CreateDirectory (dir);

        string json = JsonUtility.ToJson(currentSaveData, true);
        File.WriteAllText(dir + FileName, json);

        GUIUtility.systemCopyBuffer = dir; // Copy string to clipboard

        OnSaveGameFinish?.Invoke();

        return true;
    }

    public static void LoadGame()
    {

        string fullPath = Application.persistentDataPath + SaveDirectory + FileName;
        SaveData tempData = new SaveData();

        if (File.Exists (fullPath))
        {
            string json = File.ReadAllText (fullPath);
            tempData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.LogError("Save file does not exist.");
        }

        currentSaveData = tempData;
        OnLoadGameFinish?.Invoke();
    }
}
