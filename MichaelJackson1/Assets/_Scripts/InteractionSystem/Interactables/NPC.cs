using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteract
{
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Talking to NPC");
        return true;
    }
}