using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapReader : MonoBehaviour
{
    public CropsManager cropManager;
    [SerializeField] Tilemap tilemap;
    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        Vector3 worldPosition;
        if (mousePosition)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPosition = position;
        }

        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);
        return gridPosition;
    }

    public TileBase GetTileBase(Vector3Int gridPosition)
    {

        TileBase tile = tilemap.GetTile(gridPosition);

        return tile;
    }
}
