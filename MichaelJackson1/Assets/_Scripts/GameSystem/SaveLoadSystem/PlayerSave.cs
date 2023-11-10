using UnityEngine;

[RequireComponent(typeof(UniqueID))]

public class PlayerSave : MonoBehaviour
{
    void Awake() // Subscribe to events
    {
        SaveLoad.OnLoadGame += LoadPlayer;
        SaveLoad.OnSaveGame += SavePlayer;
    }
    private void SavePlayer()
    {
        var playerSaveData = new PlayerSaveData(transform.position); // Make sure all saved variables are included, so that when the game is saved these are written to the save file
        SaveGameManager.data.playerDictionary.Clear(); // Clear the previously saved data of this ID
        SaveGameManager.data.playerDictionary.Add(GetComponent<UniqueID>().ID, playerSaveData); // Pass the save data of this object to the related ID
    }

    private void LoadPlayer(SaveData data) // From the corresponding ID, get the data of the player and assign it the values from loaded data - expand with new variables
    {
        if (data.playerDictionary.TryGetValue(GetComponent<UniqueID>().ID, out PlayerSaveData playerData))
        {
            this.transform.position = playerData.PlayerPosition; // Set the received values to the corresponding variables
        }
    }
}

[System.Serializable]
public struct PlayerSaveData // Define all aspects that should be saved related to the player
{
    public Vector2 PlayerPosition;
    public PlayerSaveData(Vector2 _position)
    {
        PlayerPosition = _position;
    }
}
 