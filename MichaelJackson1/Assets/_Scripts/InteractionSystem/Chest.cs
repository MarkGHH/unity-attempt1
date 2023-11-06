using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InventoryHolder, IInteract
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(InventorySystem);
        return true;
    }
}
