using UnityEngine;
using UnityEngine.Tilemaps;

namespace BuildingSystem
{
    [RequireComponent (typeof (Tilemap))]
    public class TilemapLayer : MonoBehaviour
    {
        protected Tilemap tilemap {  get; private set; }

        protected void Awake()
        {
            tilemap = GetComponent<Tilemap>();
        }
    }
}