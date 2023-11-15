using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    }
}