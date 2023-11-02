using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject // Decides what data can be attributed to an item
{
    public int ID;
    public Sprite Icon;
    public int maxStackSize;
    public string displayName;
    public string typeName;
}
