using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;
    public string staticID;
    public ItemPickUpSaveData itemSaveData;

    private float dropOffset = 5;

    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemSprite.preserveAspect = true;
        ItemCount.text = "";
    }
    public void UpdateMouseSlot(InventorySlot invSlot) // Assign item to inventory slot and set the sprite, text and color
    {
        AssignedInventorySlot.AssignItem(invSlot);
        UpdateMouseSlot();

    }
    public void UpdateMouseSlot() 
    {
        ItemSprite.sprite = AssignedInventorySlot.ItemData.Icon;
        ItemCount.text = AssignedInventorySlot.StackSize.ToString();
        ItemSprite.color = Color.white;
    }

    private void Update() // If it has an item, follow the mouse position
    {
        if (AssignedInventorySlot.ItemData != null)
        {
            transform.position = Mouse.current.position.ReadValue(); // Follow the value of the mouse position

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject()) // If click, not on UI slot then drop item
            {
                if (AssignedInventorySlot.ItemData.ItemPrefab != null) // Instantiate the dropped item and generate a new staticID
                {
                    GameObject itemInstantiated = Instantiate(AssignedInventorySlot.ItemData.ItemPrefab, GameObject.FindWithTag("Player").transform.position + GameObject.FindWithTag("Player").transform.forward * dropOffset, Quaternion.identity);
                    itemInstantiated.GetComponent<StaticUniqueID>().Generate();
                }

                //the code does not deal with item stacks being dropped, it sould drop the assignedinvslot.stacksize amount

                if (AssignedInventorySlot.StackSize > 1) // If the stack size is larger than 1, reduce it by 1 otherwise wipe the slot clean
                {
                    AssignedInventorySlot.AddToStack(-1);
                    UpdateMouseSlot();
                }
                else ClearSlot();
                
            }
        }
    }
    public void ClearSlot() // Set slot to default
    {
        AssignedInventorySlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
    }
    public static bool IsPointerOverUIObject() // Show whether the mouse position is currently over UI
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult> ();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
