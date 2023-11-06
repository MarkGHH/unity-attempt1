using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour 
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem primaryInventorySystem;

    public InventorySystem InventorySystem => primaryInventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested; // When requested passes the InventorySystem of the holder

    protected virtual void Awake()
    {
        primaryInventorySystem = new InventorySystem(inventorySize); // On awake, give the gameobject an inventory system of the correct size
    }
}
