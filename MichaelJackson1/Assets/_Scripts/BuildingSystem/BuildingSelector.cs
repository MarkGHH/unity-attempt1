using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingSelector : MonoBehaviour
    {
        [SerializeField] private List<BuildableItem> buildables;
        [SerializeField] private BuildingPlacer buildingPlacer;
        private int activeBuildableIndex;
        private InputReader input;

        private void Awake()
        {
            input = gameObject.GetComponent<PlayerMovement>().input;
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}