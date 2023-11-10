using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteract
{
    [SerializeField] private string loadLevel;
    public bool Interact(Interactor interactor)
    {
        SceneManager.LoadScene(loadLevel);
        return true;
    }
}
