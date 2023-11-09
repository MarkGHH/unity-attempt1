using UnityEngine;

public class SaveLoadGameButton : MonoBehaviour
{
    public PlayerSaveData playerSaveData;
    private void Awake()
    {
        playerSaveData.GetComponent<PlayerSaveData>();
    }
    public void SaveGame()
    {
        playerSaveData.isSaving = true;
    }

    public void LoadGame()
    {
        playerSaveData.isLoading = true;
    }
}
