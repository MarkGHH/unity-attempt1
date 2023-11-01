using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class ItemPickUp : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public InventoryItemData ItemData;
    private BoxCollider2D myCollider;

    private void Awake() // On awake gets the box collider of the item and sets a radius in which the collision is triggered
    {
        myCollider = GetComponent<BoxCollider2D>();
        myCollider.isTrigger = true;
        myCollider.edgeRadius = PickUpRadius;
    }

    private void OnTriggerEnter2D(Collider2D other) // On trigger of the collider, check whether the collider had an inventory, if it does add the item to the inventory and destroy the gameobject this script is attached to
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();

        if (!inventory) return;

        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
