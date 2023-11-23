using BuildingSystem;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolController : MonoBehaviour
{
    PlayerMovement character;
    public InputReader input;
    private float offsetDistance = 1f;
    private float sizeOfHitArea = 1.2f;
    private Vector2 mousePosition;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TilemapReader tilemapReader;
    [SerializeField] CropsManager cropManager;
    [SerializeField] TileData plowableTiles;
    private float maxUseDistance = 2f;

    Vector3Int selectedTilePosition;
    bool selectable;
    bool skipUseToolGrid;


    private void Awake()
    {
        character = GetComponent<PlayerMovement>();
        input.PerformActionEvent += UseToolWorld;
        input.MousePositionEvent += MousePosition;
    }
    private void OnDisable()
    {
        input.PerformActionEvent -= UseToolWorld;
        input.MousePositionEvent -= MousePosition;
    }
    private void Update()
    {
        SelectTile();
        CanSelectCheck();
        Marker();
    }

    private void MousePosition(Vector2 vector)
    {
        mousePosition = vector;
    }

    private void SelectTile()
    {
        selectedTilePosition = tilemapReader.GetGridPosition(mousePosition, true);
    }

    public void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        selectable = Vector2.Distance(characterPosition, cameraPosition) <= maxUseDistance;
        markerManager.Show(selectable);
    }
    private void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }

    private void UseToolWorld(bool obj) // Use tool
    {
        if (obj)
        {
            Vector2 position = character.rb.position + character.moveDirection * offsetDistance;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfHitArea);
            foreach (Collider2D c in colliders)
            {
                ToolHit hit = c.GetComponent<ToolHit>();
                if (hit != null)
                {
                    hit.Hit();
                    skipUseToolGrid = true;
                }
            }

            if (!skipUseToolGrid) UseToolGrid();
            else skipUseToolGrid = false;
        }
    }

    private void UseToolGrid()
    {
        if (selectable == true)
        {
            TileBase tileBase = tilemapReader.GetTileBase(selectedTilePosition);
            TileData tileData = tilemapReader.GetTileData(tileBase);
            if (tileData != plowableTiles) return;

            if (cropManager.Check(selectedTilePosition))
            {
                cropManager.Seed(selectedTilePosition);
            }
            else
            {
                cropManager.Plow(selectedTilePosition);
            }
        }
    }

}
