using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
    Undefined,
    Tree,
    Rock
}

[CreateAssetMenu(menuName = "Data/Tool action/Gather Resource Node")]

public class GatherResourceNode : ToolAction
{
    [SerializeField] float sizeOfHitArea = 1.5f;
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;
    public override bool OnApply(Vector2 worldPoint)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfHitArea);
        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                if (hit.CanBeHit(canHitNodesOfType) == true)
                {
                    hit.Hit();
                    return true;
                }
            }
        }
        return false;
    }

}
