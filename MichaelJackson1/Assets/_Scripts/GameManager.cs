using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

//Manages the objects in the game throughout different scenes and the loading of scenes

public class GameStart : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Main");
    }
}
