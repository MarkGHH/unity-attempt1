using UnityEngine;
/// <summary>
/// This script handles the transferring of data, and should not be adjusted. In case new things should be saved, the following scripts can be adjusted:
/// - SaveData (What data is saved, and passed to this script)
/// - Save script specific to an object (Obtaining and applying values from and to the specific object based on Save/Load game). Examples are "Chest" and "PlayerSave".
/// </summary>
public class SaveGameManager : MonoBehaviour
{
    public static SaveData data;

    private void Awake()
    {
        data = new SaveData();
        SaveLoad.OnLoadGame += LoadData;
    }

    public void DeleteData()
    {
        SaveLoad.DeleteSavedData();
    }

    public static void SaveData()
    {
        var savedData = data;
        SaveLoad.Save(savedData);
    }
    public static void LoadData(SaveData _data)
    {
        data = _data;
    }
    public static void TryLoadData()
    {
        SaveLoad.Load();
    }
}
