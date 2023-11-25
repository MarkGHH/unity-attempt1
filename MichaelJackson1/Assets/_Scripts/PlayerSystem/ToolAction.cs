using UnityEngine;

public class ToolAction : ScriptableObject
{
    public virtual bool OnApply(Vector2 worldPoint)
    {
        Debug.LogWarning("OnApply is not implemented");
        return true;
    }

    public virtual bool OnApplyToTileMap(Vector3Int gridPosition, TilemapReader tilemapReader)
    {
        Debug.LogWarning("OnApplyToTileMap is not implemented");
        return true;
    }

    public virtual void OnItemUsed(InventoryDisplay inventoryDisplay, InventorySlot slot, InventorySlot_UI slotUI)
    {
        Debug.LogWarning("OnItemUsed is not implemented");
        return;
    }
}


