using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BuildingSystem
{
    public class BuildingPlacer : MonoBehaviour
    {
        [field: SerializeField] public BuildableItem ActiveBuildable { get; private set; }
        [SerializeField] private float maxBuildingDistance = 3f;
        [SerializeField] private ConstructionLayer constructionLayer;
        [SerializeField] private PreviewLayer previewLayer;
        private InputReader input;

        private bool rmbDown;
        private bool lmbDown;
        private Vector2 mousePositionWorld;
        private void Awake()
        {
            input = gameObject.GetComponent<PlayerMovement>().input;
            input.MousePositionBuildingEvent += MousePosition;
            input.PerformActionEvent += PerformAction;
            input.CancelActionEvent += CancelAction;
        }

        private void OnDisable()
        {
            input.MousePositionBuildingEvent -= MousePosition;
            input.PerformActionEvent -= PerformAction;
            input.CancelActionEvent -= CancelAction;
        }
        private void MousePosition(Vector2 mousePos)
        {
            mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePos);
        }
        private void CancelAction(bool isPressed)
        {
            rmbDown = isPressed;
        }

        private void PerformAction(bool isPressed)
        {
            lmbDown = isPressed;
        }

        private bool IsMouseWithinBuildableRange()
        {
            return Vector3.Distance(mousePositionWorld, transform.position) <= maxBuildingDistance;
        }

        private void Update()
        {
            if (!IsMouseWithinBuildableRange() || ActiveBuildable == null)
            {
                previewLayer.ClearPreview();
                return;
            }

            if (IsMouseWithinBuildableRange())
            {
                previewLayer.ShowPreview(ActiveBuildable, mousePositionWorld, constructionLayer.IsEmpty(mousePositionWorld));

                if (lmbDown && ActiveBuildable != null && constructionLayer != null && constructionLayer.IsEmpty(mousePositionWorld))
                {
                    constructionLayer.Build(mousePositionWorld, ActiveBuildable);
                }
            }

        }

        public void SetActiveBuildable(BuildableItem item)
        {
            ActiveBuildable = item;
        }

    }
}