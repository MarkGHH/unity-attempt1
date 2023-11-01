using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    public List<InventorySlot> InventorySlots => inventorySlots;

    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            InventorySlots.Add(new InventorySlot()); // Create inventory slots based on the amount of slots passed through from the inventory system attached to the game object (player/chest)
        }
    }
    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd) // When a player picks up an item
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) // Check whether item exists in inventory already
        {
            foreach (var slot in invSlot) // Check whether the found item has room left in the stack for each time that the item is found in the inventory
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }

        if (HasFreeSlot(out InventorySlot freeSlot)) // Gets the first available slot if the item doesn't exist already.
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot) // Receives an itemData and checks whether that itemData is already in the inventory
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        Debug.Log(invSlot.Count);
        return invSlot == null ? false : true; // If at least 1 stack is found of the item, return true otherwise return false (item not found)
    }  

    public bool HasFreeSlot(out InventorySlot freeSlot) // Check whether there is a free spot available
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }
}
