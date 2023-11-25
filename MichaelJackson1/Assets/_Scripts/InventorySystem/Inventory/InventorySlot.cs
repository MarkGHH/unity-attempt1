using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private ItemData itemData; // Reference to the item data
    [SerializeField] private int stackSize; // Current stack size - how many of the item do we have?
    public ItemData ItemData => itemData;
    public int StackSize => stackSize;
    public InventorySlot(ItemData source, int amount) // Constructor to make an occupied inventory slot
    {
        itemData = source;
        stackSize = amount;
    }
    public InventorySlot() // Constructor to make an empty inventory slot
    {
        ClearSlot(); 
    }
    public void ClearSlot() // Clears the slot; sets itemdata to null and stacksize to -1
    {
        itemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot) // Assigns an item to the slot
    {
        if (itemData == invSlot.ItemData) AddToStack(invSlot.stackSize); // Does the slot contain the same item we are trying to assign to it? Then add to the stack
        else // Overwrites slot with the inventory slot that we are passing in
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }
    public void UpdateInventorySlot(ItemData data, int amount) // Updates slot directly
    {
        itemData = data; 
        stackSize = amount; 
    }
    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining) // Checks the remaining amount including a return of the remaining amount
    {
        amountRemaining = ItemData.maxStackSize - stackSize;

        return EnoughRoomLeftInStack(amountToAdd);
    }
    public bool EnoughRoomLeftInStack(int amountToAdd) // Check whether the amount the player tries to add, in addition to the current stack size, is larger than max stack size
    {
        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.maxStackSize) return true;
        else return false;
    }
    public void AddToStack(int amount) // Add a given amount from the selected slot
    {
        stackSize += amount;
    }
    public void RemoveFromStack(int amount) // Remove a given amount from the selected slot
    {
        stackSize -= amount;
    }
    public bool SplitStack(out InventorySlot splitStack) // Is there enough to actually split? If not, return false
    {
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }
         
        int halfStack = Mathf.RoundToInt(StackSize / 2); // Get half the stack size and remove that from the current slot
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack); // Creates a copy of this slot with half the stack size
        return true;
    }
}
