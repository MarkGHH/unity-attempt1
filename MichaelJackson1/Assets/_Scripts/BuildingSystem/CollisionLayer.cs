using UnityEngine;
using UnityEngine.Tilemaps;

namespace BuildingSystem
{
    public class CollisionLayer : TilemapLayer
    {
        [SerializeField] private TileBase collisionTileBase;

        public void SetCollisions(Buildable buildable, bool value)
        {
            var tile = value ? collisionTileBase : null;
            buildable.IterateCollisionSpace(tileCoordinates => tilemap.SetTile(tileCoordinates, tile));
        }
    }
}

