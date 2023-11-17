using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class InventoryHolder : MonoBehaviour 
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem primaryInventorySystem;
    [SerializeField] protected int offset = 10;

    public int Offset => offset;

    public InventorySystem InventorySystem => primaryInventorySystem;

    public static UnityAction<InventorySystem, int> OnDynamicInventoryDisplayRequested; // When requested passes the InventorySystem of the holder

    protected virtual void Awake()
    {
        SaveLoad.OnLoadGame += LoadInventory;
        primaryInventorySystem = new InventorySystem(inventorySize); // On awake, give the gameobject an inventory system of the correct size
    }

    protected abstract void LoadInventory(SaveData saveData);
}

[System.Serializable]
public struct InventorySaveData // Define all aspects that should be saved related to the chest
{
    public InventorySystem invSystem;
    public Vector2 position;

    public InventorySaveData(InventorySystem _invSystem, Vector2 _position)
    {
        invSystem = _invSystem;
        position = _position;
    }

    public InventorySaveData(InventorySystem _invSystem)
    {
        invSystem = _invSystem;
        position = Vector3.zero;
    }
}
