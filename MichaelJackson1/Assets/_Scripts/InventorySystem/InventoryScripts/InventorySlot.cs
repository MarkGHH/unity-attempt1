using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int stackSize;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;
    public InventorySlot(InventoryItemData source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }
    public InventorySlot() // On creation clears the slot
    {
        ClearSlot(); 
    }
    public void ClearSlot() // Sets itemdata to null and stacksize to -1
    {
        itemData = null;
        stackSize = -1;
    }
    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining) // Checks the remaining amount including a return of the remaining amount
    {
        amountRemaining = ItemData.maxStackSize - stackSize;

        return RoomLeftInStack(amountToAdd);
    }
    public void UpdateInventorySlot(InventoryItemData data, int amount) 
    {
        itemData = data; stackSize = amount;
    }
    public bool RoomLeftInStack(int amountToAdd) // Check whether the amount the player tries to add in addition to the current stack size is larger than max stack size
    {
        if (stackSize + amountToAdd <= itemData.maxStackSize) return true;
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
}
