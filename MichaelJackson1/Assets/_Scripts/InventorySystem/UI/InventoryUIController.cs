using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Windows;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
    public DynamicInventoryDisplay playerBackpackPanel;

    private void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
    }
    // Opening inventory and chests
    void DisplayInventory(InventorySystem invToDisplay, int offset, bool isOpen)
    {
        inventoryPanel.gameObject.SetActive(isOpen);
        inventoryPanel.RefreshDynamicInventory(invToDisplay, offset);
    }

    void DisplayPlayerInventory(InventorySystem invToDisplay, int offset, bool displayMode)
    {
        playerBackpackPanel.gameObject.SetActive(displayMode);
        playerBackpackPanel.RefreshDynamicInventory(invToDisplay, offset);
    }
}
