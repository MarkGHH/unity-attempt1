using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(StaticUniqueID))]

public class ItemPickUp : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public InventoryItemData ItemData;
    private BoxCollider2D myCollider;

    [SerializeField] public ItemPickUpSaveData itemSaveData;
    private string staticID;

    private void Awake() // On awake gets the box collider of the item and sets a radius in which the collision is triggered
    {
        SaveLoad.OnLoadGame += LoadGame;
        itemSaveData = new ItemPickUpSaveData(ItemData, transform.position);

        myCollider = GetComponent<BoxCollider2D>();
        myCollider.isTrigger = true;
        myCollider.edgeRadius = PickUpRadius;
    }

    private void Start()
    {
        staticID = GetComponent<StaticUniqueID>().ID;
        SaveGameManager.data.activeItems.Add(staticID, itemSaveData);
    }

    private void LoadGame(SaveData data)
    {
        if (data.collectedItems.Contains(staticID)) Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (SaveGameManager.data.activeItems.ContainsKey(staticID)) SaveGameManager.data.activeItems.Remove(staticID);
        SaveLoad.OnLoadGame -= LoadGame;
    }

    private void OnTriggerEnter2D(Collider2D other) // On trigger of the collider, check whether the collider had an inventory, if it does add the item to the inventory and destroy the gameobject this script is attached to
    {
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();

        if (!inventory) return;

        if (inventory.AddToInventory(ItemData, 1))
        {
            AudioManager.Instance.PlaySFX("PickItem");
            SaveGameManager.data.collectedItems.Add(staticID);
            Destroy(this.gameObject);

        }
    }
}

[System.Serializable]
public struct ItemPickUpSaveData
{
    public InventoryItemData ItemData;
    public Vector2 Position;

    public ItemPickUpSaveData(InventoryItemData itemData, Vector2 position)
    {
        ItemData = itemData;
        Position = position;
    }
}