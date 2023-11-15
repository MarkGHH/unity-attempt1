using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BuildingSystem
{
    public class ConstructionLayer : TilemapLayer
    {
        private Dictionary<Vector3Int, Buildable> buildables = new();
        public void Build(Vector3 worldCoordinates, BuildableItem item)
        {
            var coordinates = tilemap.WorldToCell(worldCoordinates);
            var buildable = new Buildable(item, coordinates, tilemap);

            if (item.Tile != null)
            {
                var tileChangeData = new TileChangeData(coordinates, item.Tile, Color.white, Matrix4x4.Translate(item.TileOffset));
                tilemap.SetTile(tileChangeData, false);
            }
            buildables.Add(coordinates, buildable);
        }

        public bool IsEmpty(Vector3 worldCoordinates)
        {
            var coordinates = tilemap.WorldToCell(worldCoordinates);
            return !buildables.ContainsKey(coordinates) && tilemap.GetTile(coordinates) == null;
        }
    }
}