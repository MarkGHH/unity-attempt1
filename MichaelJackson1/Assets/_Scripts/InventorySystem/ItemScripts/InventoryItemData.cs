using UnityEngine;
/// <summary>
/// This is a scriptable object, that defines what an item is in our game.
/// It could be inherited from to have branched version of items, for example potions and equipment.
/// </summary>


[CreateAssetMenu(menuName ="Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject // Decides what data can be attributed to an item
{
    public int ID;
    public Sprite Icon;
    public int maxStackSize;
    public string displayName;
    public string typeName;
    public GameObject ItemPrefab;

    public void UseItem()
    {
        Debug.Log("Using Item");
    }
}

