using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSaveData : MonoBehaviour
{
    private PlayerData playerData = new PlayerData();
    [SerializeField] private SaveLoadGameButton SaveButton;
    [SerializeField] private SaveLoadGameButton LoadButton;

    private void Awake()
    {
        SaveButton.OnSavetheGame += setSaveData;
        LoadButton.OnLoadtheGame += setLoadData;
    }
    private void OnDisable()
    {
        SaveButton.OnSavetheGame -= setSaveData;
        LoadButton.OnLoadtheGame -= setLoadData;
    }
    void setSaveData()
    {
        playerData.PlayerPosition = transform.position; // Declare all variables from the player to the data - this is required for each object that requires having it's data saved
        SaveGameManager.currentSaveData.PlayerData = playerData; 
        SaveGameManager.SaveGame(); // Set the current data to the save file
    }

    void setLoadData()
    {
        SaveGameManager.LoadGame(); // Get the saved data from the save file
        playerData = SaveGameManager.currentSaveData.PlayerData; 
        transform.position = playerData.PlayerPosition; // Declare all variables from the data to the player - this is required for each object that requires having it's data saved

    }
}

[System.Serializable]
public struct PlayerData
{
    public Vector3 PlayerPosition;
    // public int CurrentHealth;
}



//public bool isSaving;
//public bool isLoading;

/*void Update()
{
    if (Keyboard.current.rKey.wasPressedThisFrame || isSaving == true)
    {
        MyData.PlayerPosition = transform.position; // Declare all variables from the player to the data - this is required for each object that requires having it's data saved
        SaveGameManager.currentSaveData.PlayerData = MyData;
        SaveGameManager.SaveGame();
        isSaving = false;
    }

    if (Keyboard.current.tKey.wasPressedThisFrame || isLoading == true)
    {

        MyData = SaveGameManager.currentSaveData.PlayerData;
        transform.position = MyData.PlayerPosition; // Declare all variables from the data to the player - this is required for each object that requires having it's data saved
        SaveGameManager.LoadGame();
        isLoading = false;
    }
}*/