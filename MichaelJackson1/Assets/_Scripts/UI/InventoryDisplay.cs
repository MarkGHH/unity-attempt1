using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay);
    protected virtual void UpdateSlot(InventorySlot updatedSlot) // Loop through slots in the dictionary, if the value matches then update that slot
    {
        foreach (var slot in slotDictionary) 
        { 
            if (slot.Value == updatedSlot) // Slot value = the inventory slot from the system
            {
                slot.Key.UpdateUISlot(updatedSlot); // Slot Key = the UI representation of the value
            }
        }
    }
    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null) // Clicked slot has an item - mouse doesn't have an item -> pick up that item
        {
            // If player is holding shift key? -> Split the stack
            mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
            clickedUISlot.ClearSlot();
            return;
        }

        if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)  // Clicked slot doesn't have an item - mouse does have an item -> place mouse item into empty slot
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
        }

        // Both slots have an item -> decide what to do..
        // Are both items the same? -> combine stacks
        // Is the slot stack size + mouse stack size > the slot max stack size? -> take remainder from mouse
        // Are items different? -> swap the items

    }
}
