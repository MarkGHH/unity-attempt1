using UnityEngine;
[RequireComponent(typeof(UniqueID))]

public class Chest : InventoryHolder, IInteract
{
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(InventorySystem);
        return true;
    }
    protected override void Awake()
    {
        base.Awake();
        SaveLoad.OnLoadGame += LoadInventory;
    }

    private void Start()
    {
        var chestSaveData = new ChestSaveData(primaryInventorySystem, transform.position);

        SaveGameManager.data.chestDictionary.Add(GetComponent<UniqueID>().ID, chestSaveData);
    }

    private void LoadInventory(SaveData data)
    {
        if (data.chestDictionary.TryGetValue(GetComponent<UniqueID>().ID, out ChestSaveData chestData))
        {
            this.primaryInventorySystem = chestData.invSystem;
            this.transform.position = chestData.position;
        }
    }
}

[System.Serializable]
public struct ChestSaveData
{
    public InventorySystem invSystem;
    public Vector2 position;

    public ChestSaveData(InventorySystem _invSystem, Vector2 _position)
    {
        invSystem = _invSystem;
        position = _position;
    }
}
