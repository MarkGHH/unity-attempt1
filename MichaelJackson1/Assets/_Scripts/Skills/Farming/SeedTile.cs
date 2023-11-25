using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool action/Seed tile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TilemapReader tilemapReader)
    {
        if (tilemapReader.cropManager.Check(gridPosition) == false)
        {
            return false;
        }

        tilemapReader.cropManager.Seed(gridPosition);
        return true;
    }

    public override void OnItemUsed(InventoryDisplay inventoryDisplay, InventorySlot slot, InventorySlot_UI slotUI)
    {
        slot.RemoveFromStack(1); // Reduce by 1
        inventoryDisplay.UpdateHotbarSlot(slot); // Update hotbar
        slotUI.UpdateUISlot(slot); // Update InventorySlot
        return;
    }
}
