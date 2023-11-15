using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BuildingSystem
{
    [CreateAssetMenu(menuName = "Building/New buildable item", fileName = "New buildable item")]

    public class BuildableItem : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public TileBase Tile { get; private set; }
        [field: SerializeField] public Vector3 TileOffset { get; private set; }
        [field: SerializeField] public Sprite PreviewSprite { get; private set; }

    }
}


