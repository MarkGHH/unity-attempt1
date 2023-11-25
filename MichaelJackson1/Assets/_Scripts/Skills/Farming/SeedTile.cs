using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool action/Seed tile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TilemapReader tilemapReader)
    {
        if (tilemapReader.cropManager.Check(gridPosition) == false)
        {
            return false;
        }

        tilemapReader.cropManager.Seed(gridPosition);
        return true;
    }
}
