using UnityEngine;

public class ToolController : MonoBehaviour
{
    public InputReader input;
    Animator animator;
    [SerializeField] HotbarDisplay hotbarDisplay;
    private Vector2 mousePosition;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TilemapReader tilemapReader;
    private float maxUseDistance = 2f;
    private ItemData currentItem;

    Vector3Int selectedTilePosition;
    bool selectable;


    private void Awake()
    {
        input.PerformActionEvent += UseTool;
        input.MousePositionEvent += MousePosition;
        animator = GetComponent<Animator>();
    }
    private void OnDisable()
    {
        input.PerformActionEvent -= UseTool;
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

    private void UseTool(bool obj)
    {
        if (obj)
            currentItem = hotbarDisplay.CurrentItem();
        {
            if (currentItem != null)
            {
                if (currentItem.onAction != null) UseToolWorld();
                else if (currentItem.onTileMapAction != null && selectable) UseToolGrid();
            }
        }
    }
    private void UseToolWorld()
    {
        Vector2 characterPosition = transform.position;
        hotbarDisplay.CurrentItem().onAction.OnApply(characterPosition);
        animator.SetTrigger("Act");
    }

    private void UseToolGrid()
    {
        hotbarDisplay.CurrentItem().onTileMapAction.OnApplyToTileMap(selectedTilePosition, tilemapReader);
        hotbarDisplay.CurrentSlot().RemoveFromStack(1);
        animator.SetTrigger("Act");
    }
}
