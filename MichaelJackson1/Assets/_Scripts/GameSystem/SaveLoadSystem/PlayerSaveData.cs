using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerSaveData : MonoBehaviour
{
    private PlayerData MyData = new PlayerData();
    private bool isSaving;
    private bool isLoading;

    private void Start()
    {
        // Subscribe to events, to bool to true

    }
    void Update()
    {
        MyData.PlayerPosition = transform.position; // Declare all variables from the player, as the script is attached to the player object
        
        //On Save (isSaving == true);
        SaveGameManager.currentSaveData.PlayerData = MyData; // Saving
        //On Save (isSaving == false);


        //On Load (isLoading == true);
        MyData = SaveGameManager.currentSaveData.PlayerData; // Loading -> won't work because save is in the same method
        transform.position = MyData.PlayerPosition;
        //On Load (isLoading == false);
    }
}

[System.Serializable]
public struct PlayerData
{
    public Vector3 PlayerPosition;
    // public int CurrentHealth;
}
