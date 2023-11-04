using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

//Manages the objects in the game throughout different scenes and the loading of scenes

public class GameStart : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject pauseMenu; // Should contain the UI

    private void Start()
    {
        pauseMenu.SetActive(false);
        input.PauseEvent += HandlePause;
        input.ResumeEvent += HandleResume;
    }
    private void HandlePause()
    {
        pauseMenu.SetActive(true);
    }
    private void HandleResume()
    {
        pauseMenu.SetActive(false);
    }
}
