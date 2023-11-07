using UnityEngine;

public class SaveLoadGameButton : MonoBehaviour
{
    public void SaveGame()
    {
        SaveGameManager.SaveGame();
    }

    public void LoadGame()
    {
        SaveGameManager.LoadGame();
    }
}
