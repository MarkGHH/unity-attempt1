using Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BuildingSystem
{
    public class ConstructionLayer : TilemapLayer
    {
        private Dictionary<Vector3Int, Buildable> buildables = new();
        [SerializeField] private CollisionLayer collisionLayer;
        public void Build(Vector3 worldCoordinates, BuildableItem item)
        {
            GameObject itemObject = null;

            var coordinates = tilemap.WorldToCell(worldCoordinates);



            if (item.Tile != null)
            {
                var tileChangeData = new TileChangeData(coordinates, item.Tile, Color.white, Matrix4x4.Translate(item.TileOffset));
                tilemap.SetTile(tileChangeData, false);
            }

            if (item.GameObject != null)
            {
                itemObject = Instantiate(item.GameObject, tilemap.CellToWorld(coordinates) + tilemap.cellSize /2 + item.TileOffset, Quaternion.identity);
            }

            var buildable = new Buildable(item, coordinates, tilemap, itemObject);
            if (item.UseCustomCollisionSpace)
            {
                collisionLayer.SetCollisions(buildable, true);
                RegisterBuildableCollisionSpace(buildable);
            }
            else
            {
                buildables.Add(coordinates, buildable);
            }

        }

        public void Destroy(Vector3 worldCoordinates)
        {
            var coordinates = tilemap.WorldToCell(worldCoordinates);
            if (!buildables.ContainsKey(coordinates)) return;

            var buildable = buildables[coordinates];
            if (buildable.BuildableType.UseCustomCollisionSpace)
            {
                collisionLayer.SetCollisions(buildable, false);
                UnregisterBuildableCollisionSpace(buildable);
            }
            buildables.Remove(coordinates);
            buildable.Destroy();
        }

        public bool IsEmpty(Vector3 worldCoordinates, RectInt collisionSpace = default)
        {
            var coordinates = tilemap.WorldToCell(worldCoordinates);
            if (!collisionSpace.Equals(default))
            {
                return !IsRectOccupied(coordinates, collisionSpace);
            }
            return !buildables.ContainsKey(coordinates) && tilemap.GetTile(coordinates) == null;
        }

        private void RegisterBuildableCollisionSpace(Buildable buildable)
        {
            buildable.IterateCollisionSpace(tileCoordinates => buildables.Add(tileCoordinates, buildable));
        }

        private void UnregisterBuildableCollisionSpace(Buildable buildable)
        {
            buildable.IterateCollisionSpace(tileCoordinates =>
            {
                buildables.Remove(tileCoordinates);
            });
        }
        private bool IsRectOccupied(Vector3Int coordinates, RectInt rect)
        {
            return rect.Iterate(coordinates, tileCoordinates => buildables.ContainsKey(tileCoordinates));
        }
    }
}