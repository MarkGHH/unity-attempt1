using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;
    [SerializeField] private GameObject _slotHighlight;

    private Button button;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ClearSlot();

        itemSprite.preserveAspect = true;

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick); // Get the button and subscribe it to the method

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    public void ToggleHighlight()
    {
        _slotHighlight.SetActive(!_slotHighlight.activeInHierarchy);
    }

    public void UpdateUISlot(InventorySlot slot) // Update the UI slot and pass in the inventory slot to display on the UI
    {
        if (slot.ItemData != null)
        {
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;

            if (slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
            else itemCount.text = "";
        }
        else
        {
            ClearSlot();
        }
    }

    public void UpdateUISlot() // Update the UI slot without input
    {
        if (assignedInventorySlot != null) UpdateUISlot(assignedInventorySlot);
    }

    public void ClearSlot() // Empty the slot
    {
        assignedInventorySlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }
    public void OnUISlotClick() // Track whether the slot is clicked
    {
        ParentDisplay?.SlotClicked(this);
    }
}
