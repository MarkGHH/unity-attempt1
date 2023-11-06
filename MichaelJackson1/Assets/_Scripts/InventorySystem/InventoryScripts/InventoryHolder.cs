using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour 
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem inventorySystem;

    public InventorySystem InventorySystem => inventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested; // When requested passes the InventorySystem of the holder

    private void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize); // On awake, give the gameobject an inventory system of the correct size
    }
}
