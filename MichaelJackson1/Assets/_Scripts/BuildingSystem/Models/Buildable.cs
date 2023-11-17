using Extensions;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace BuildingSystem
{
    [Serializable] public class Buildable
    {
        [field: SerializeField] public Tilemap ParentTilemap { get; private set; }
        [field: SerializeField] public BuildableItem BuildableType { get; private set; }
        [field: SerializeField] public GameObject GameObject { get; private set; }
        [field: SerializeField] public Vector3Int Coordinates { get; private set; }

        public Buildable(BuildableItem type, Vector3Int coordinates, Tilemap tilemap, GameObject obj = null)
        {
            BuildableType = type;
            ParentTilemap = tilemap;
            Coordinates = coordinates;
            GameObject = obj;
        }

        public void Destroy()
        {
            if (GameObject != null)
            {
                Object.Destroy(GameObject);
            }
            ParentTilemap.SetTile(Coordinates, null);
        }

        public void IterateCollisionSpace(RectIntExtensions.RectAction action)
        {
            BuildableType.CollisionSpace.Iterate(Coordinates, action);
        }

        public bool IterateCollisionSpace(RectIntExtensions.RectActionBool action)
        {
            return BuildableType.CollisionSpace.Iterate(Coordinates, action);
        }
    }
}