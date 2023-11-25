using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(StaticUniqueID))]

public class ItemPickUp : MonoBehaviour
{
    private float PickUpRadius = 3f;
    private float PickUpSpeed = 8f;
    public ItemData ItemData;
    private BoxCollider2D myCollider;

    [SerializeField] public ItemPickUpSaveData itemSaveData;
    private string staticID;
    private Transform playerTransform;
    private PlayerInventoryHolder playerInventory;

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
        playerTransform = GameManager.instance.player.transform;
        playerInventory = GameManager.instance.player.GetComponentInParent<PlayerInventoryHolder>();
    }

    private void OnDestroy()
    {
        if (SaveGameManager.data.activeItems.ContainsKey(staticID)) SaveGameManager.data.activeItems.Remove(staticID);
        SaveLoad.OnLoadGame -= LoadGame;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);


        if (distance > PickUpRadius) return;
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, PickUpSpeed * Time.deltaTime);
        if (distance < 0.1f) PickUpItem();

    }

    private void PickUpItem()
    {
        var inventory = playerInventory;

        if (!inventory) return;

        if (inventory.AddToInventory(ItemData, 1))
        {
            AudioManager.Instance.PlaySFX("PickItem");
            SaveGameManager.data.collectedItems.Add(staticID);
            Destroy(this.gameObject);

        }
    }

    private void LoadGame(SaveData data)
    {
        if (data.collectedItems.Contains(staticID)) Destroy(this.gameObject);
    }
}

[System.Serializable]
public struct ItemPickUpSaveData
{
    public ItemData ItemData;
    public Vector2 Position;

    public ItemPickUpSaveData(ItemData itemData, Vector2 position)
    {
        ItemData = itemData;
        Position = position;
    }
}