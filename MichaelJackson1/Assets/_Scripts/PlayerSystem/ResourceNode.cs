using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : ToolHit
{
    [SerializeField] GameObject ItemPickUp;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 1f;
    [SerializeField] ResourceNodeType nodeType;
    public override void Hit()
    {
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;

            GameObject go = Instantiate(ItemPickUp);
            go.GetComponent<StaticUniqueID>().Generate();
            go.transform.position = position;   
        }
        Destroy(gameObject);
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}
