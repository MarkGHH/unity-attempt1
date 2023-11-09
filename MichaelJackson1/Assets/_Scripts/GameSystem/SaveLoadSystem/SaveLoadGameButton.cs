using UnityEngine;
using static SaveLoadGameButton;

public class SaveLoadGameButton : MonoBehaviour
{
    public PlayerSaveData playerSaveData;

    public delegate void SavetheGame();
    public event SavetheGame OnSavetheGame;
    public delegate void LoadtheGame();
    public event LoadtheGame OnLoadtheGame;

    private void Awake()
{
    playerSaveData.GetComponent<PlayerSaveData>();
}
    public void SaveGame()
    {
        OnSavetheGame.Invoke();
    }

    public void LoadGame()
    {
        OnLoadtheGame.Invoke();
    }
}




/*
public void SaveGame()
{
    //playerSaveData.isSaving = true;
}

public void LoadGame()
{
    //playerSaveData.isLoading = true;
}*/