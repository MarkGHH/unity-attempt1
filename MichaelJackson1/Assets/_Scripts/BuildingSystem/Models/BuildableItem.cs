using UnityEngine;
using UnityEngine.Tilemaps;

// https://www.youtube.com/watch?v=Bhx_v5vieAU

namespace BuildingSystem
{
    [CreateAssetMenu(menuName = "Building/New buildable item", fileName = "New buildable item")]

    public class BuildableItem : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public TileBase Tile { get; private set; }
        [field: SerializeField] public Vector3 TileOffset { get; private set; }
        [field: SerializeField] public Sprite PreviewSprite { get; private set; }
        [field: SerializeField] public Sprite UiIcon { get; private set; }
        [field: SerializeField] public GameObject GameObject { get; private set; }
        [field: SerializeField] public RectInt CollisionSpace { get; private set; }
        [field: SerializeField] public bool UseCustomCollisionSpace { get; private set; }

    }
}


