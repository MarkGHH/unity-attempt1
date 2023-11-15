using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class PreviewLayer : TilemapLayer
    {
        [SerializeField] private SpriteRenderer previewRenderer;

        public void ShowPreview(BuildableItem item, Vector3 worldCoordinates, bool isValid)
        {
            var coordinates = tilemap.WorldToCell(worldCoordinates);
            previewRenderer.enabled = true;
            previewRenderer.transform.position = tilemap.CellToWorld(coordinates) + tilemap.cellSize / 2 + item.TileOffset;
            previewRenderer.sprite = item.PreviewSprite;
            previewRenderer.color = isValid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
        }

        public void ClearPreview()
        {
            previewRenderer.enabled = false;
        }
    }
}