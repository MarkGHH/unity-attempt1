using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{

    public static UnityAction OnPlayerInventoryChanged; // When requested passes the InventorySystem of the holder
    public static UnityAction<InventorySystem, int, bool> OnPlayerInventoryDisplayRequested;
    public InputReader input;
    public bool displayMode;

    private void Start()
    {
        input = gameObject.GetComponent<PlayerMovement>().input;
        SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
    }
    private void OnEnable()
    {
        input.BackpackEvent += HandleBackpack;
    }

    private void OnDisable()
    {
        input.BackpackEvent -= HandleBackpack;
    }

    private void HandleBackpack()
    {
        if (displayMode == false) displayMode = true;
        else if (displayMode == true) displayMode = false;
        OnPlayerInventoryDisplayRequested?.Invoke(primaryInventorySystem, offset, displayMode);
    }

    protected override void LoadInventory(SaveData data) // From the corresponding ID, get the data of the chest and assign it the values from loaded data
    {
        if (data.playerInventory.invSystem != null)
        {
            this.primaryInventorySystem = data.playerInventory.invSystem;
            OnPlayerInventoryChanged?.Invoke();
        }
    }
    public bool AddToInventory(InventoryItemData data, int amount) // Attempt to add the item to the primary inventory system, which is attached to the player of type InventorySystem
    {
        if (primaryInventorySystem.AddToInventory(data, amount)) return true;

        return false;
    }


}
