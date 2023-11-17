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

    void Update() // old input system, how should this work?? test at a later stage and adjust
    {
        if (inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            inventoryPanel.gameObject.SetActive(false);
        }
        if (playerBackpackPanel.gameObject.activeInHierarchy && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            playerBackpackPanel.gameObject.SetActive(false);
        }
    }

    // Opening inventory and chests
    void DisplayInventory(InventorySystem invToDisplay, int offset)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay, offset);
    }

    void DisplayPlayerInventory(InventorySystem invToDisplay, int offset, bool displayMode)
    {
        playerBackpackPanel.gameObject.SetActive(displayMode);
        playerBackpackPanel.RefreshDynamicInventory(invToDisplay, offset);
    }
}
