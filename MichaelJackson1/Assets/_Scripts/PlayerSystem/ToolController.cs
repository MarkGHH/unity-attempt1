using UnityEngine;

public class ToolController : MonoBehaviour
{
    public InputReader input;
    Animator animator;

    private Vector2 mousePosition;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TilemapReader tilemapReader;

    [SerializeField] InventoryDisplay inventoryDisplay;
    [SerializeField] InventorySlot_UI inventorySlot_UI;
    [SerializeField] HotbarDisplay hotbarDisplay;

    private float maxUseDistance = 2f;
    private ItemData currentItem;
    private InventorySlot currentSlot;

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
        { 
            currentItem = hotbarDisplay.CurrentItem();
            currentSlot = hotbarDisplay.CurrentSlot();
            if (currentItem != null)
            {
                if (currentItem.onAction != null) UseToolWorld(currentItem, currentSlot);
                else if (currentItem.onTileMapAction != null && selectable) UseToolGrid(currentItem, currentSlot);
            }
        }
    }
    private void UseToolWorld(ItemData currentItem, InventorySlot currentSlot)
    {
        Vector2 characterPosition = transform.position;
        bool completed = currentItem.onAction.OnApply(characterPosition);
        animator.SetTrigger("Act");

        if (completed)
        {
            if (currentItem.onItemUsed != null)
            {
                currentItem.onTileMapAction.OnItemUsed(inventoryDisplay, currentSlot, inventorySlot_UI);
            }
        }
    }

    private void UseToolGrid(ItemData currentItem, InventorySlot currentSlot)
    {
        bool completed = currentItem.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tilemapReader);
        animator.SetTrigger("Act");

        if (completed) 
        { 
            if (currentItem.onItemUsed != null)
            {
                currentItem.onTileMapAction.OnItemUsed(inventoryDisplay, currentSlot, inventorySlot_UI);
            }
        }
    }
}
