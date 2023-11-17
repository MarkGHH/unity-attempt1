using System;
using UnityEngine;

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

        public event Action ActiveBuildableChanged;

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
            if (!IsMouseWithinBuildableRange() || constructionLayer == null)
            {
                previewLayer.ClearPreview();
                return;
            }
            if (rmbDown) constructionLayer.Destroy(mousePositionWorld);

            if (ActiveBuildable == null) return;

            var isSpaceEmpty = constructionLayer.IsEmpty(mousePositionWorld, ActiveBuildable.UseCustomCollisionSpace ? ActiveBuildable.CollisionSpace : default);
            
            if (IsMouseWithinBuildableRange())
            {
                previewLayer.ShowPreview(ActiveBuildable, mousePositionWorld, isSpaceEmpty);

                if (lmbDown && isSpaceEmpty)
                {
                    constructionLayer.Build(mousePositionWorld, ActiveBuildable);
                }
            }

        }

        public void SetActiveBuildable(BuildableItem item)
        {
            ActiveBuildable = item;
            ActiveBuildableChanged?.Invoke();
        }

    }
}