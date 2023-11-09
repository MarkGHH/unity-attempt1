using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSaveData : MonoBehaviour
{
    private PlayerData MyData = new PlayerData();
    public bool isSaving;
    public bool isLoading;

    void Update()
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
    }
}

[System.Serializable]
public struct PlayerData
{
    public Vector3 PlayerPosition;
    // public int CurrentHealth;
}

