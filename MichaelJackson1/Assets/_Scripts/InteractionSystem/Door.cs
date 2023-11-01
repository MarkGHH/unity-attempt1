using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, InteractInterface
{
    [SerializeField] private string prompt;
    [SerializeField] private string loadLevel;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Opening door");
        SceneManager.LoadScene(loadLevel);
        return true;
    }
}
