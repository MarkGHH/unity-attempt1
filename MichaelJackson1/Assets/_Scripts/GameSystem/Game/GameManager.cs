using UnityEngine;
using System;

//Manages the objects in the game throughout different scenes and the loading of scenes

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject UIMenu; // Should contain the UI
    [SerializeField] private GameObject buildingMenu; // Should contain the UI

    private void Awake()
    {
        UIMenu.SetActive(false);
        input.UIModeEvent += HandleEnterUI;
        input.ExitUIEvent += HandleExitUI;
        input.ExitBuildingEvent += HandleExitBuilding;
        input.BuildingModeEvent += HandleEnterBuilding;
    }
    private void OnDisable()
    {
        input.UIModeEvent -= HandleEnterUI;
        input.ExitUIEvent -= HandleExitUI;
        input.ExitBuildingEvent -= HandleExitBuilding;
        input.BuildingModeEvent -= HandleEnterBuilding;
    }
    private void HandleEnterBuilding()
    {
        buildingMenu.SetActive(true);
    }
    private void HandleExitBuilding()
    {
        buildingMenu.SetActive(false);
    }
    private void HandleEnterUI()
    {
        UIMenu.SetActive(true);
    }
    private void HandleExitUI()
    {
        UIMenu.SetActive(false);
    }

}
