using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This is a scriptable object, that defines what an item is in our game.
/// It could be inherited from to have branched version of items, for example potions and equipment.
/// </summary>


[CreateAssetMenu(menuName ="Inventory System/Inventory Item")]
public class ItemData : ScriptableObject // Decides what data can be attributed to an item
{
    public int ID;
    public Sprite Icon;
    public int maxStackSize;
    public string displayName;
    public ItemType ItemType;
    public GameObject ItemPrefab;
    public ToolAction onAction;
    public ToolAction onTileMapAction;
    public ToolAction onItemUsed;
}

public enum ItemType
{
    Loot,
    Tool,
    Weapon,
    Consumable
}

