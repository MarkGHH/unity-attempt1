using UnityEngine;
[RequireComponent(typeof(UniqueID))]

public class Chest : InventoryHolder, IInteract
{
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

    private void LoadInventory(SaveData data) // From the corresponding ID, get the data of the chest and assign it the values from loaded data
    {
        if (data.chestDictionary.TryGetValue(GetComponent<UniqueID>().ID, out ChestSaveData chestData))
        {
            this.primaryInventorySystem = chestData.invSystem;
            this.transform.position = chestData.position;
        }
    }
}



[System.Serializable]
public struct ChestSaveData // Define all aspects that should be saved related to the chest
{
    public InventorySystem invSystem;
    public Vector2 position;

    public ChestSaveData(InventorySystem _invSystem, Vector2 _position)
    {
        invSystem = _invSystem;
        position = _position;
    }
}
