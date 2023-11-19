
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tile/Tile Data")]
public class TileData : ScriptableObject
{
    public List<TileBase> tiles;
    public bool plowable;
}
