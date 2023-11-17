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
            input.NextItemEvent += NextItem;
        }

        private void OnDisable()
        {
            input.NextItemEvent -= NextItem;
        }
        private void NextItem()
        {
            activeBuildableIndex = (activeBuildableIndex + 1) % buildables.Count;
            buildingPlacer.SetActiveBuildable(buildables[activeBuildableIndex]);
        }

    }
}