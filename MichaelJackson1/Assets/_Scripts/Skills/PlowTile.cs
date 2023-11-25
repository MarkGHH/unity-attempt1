using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow")]
public class PlowTile : ToolAction
{
    [SerializeField] List<TileBase> canPlow;
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TilemapReader tilemapReader)
    {
        TileBase tileToPlow = tilemapReader.GetTileBase(gridPosition);
        if (canPlow.Contains(tileToPlow) == false)
        {
            return false;
        }

        tilemapReader.cropManager.Plow(gridPosition);

        return true;
    }
}
