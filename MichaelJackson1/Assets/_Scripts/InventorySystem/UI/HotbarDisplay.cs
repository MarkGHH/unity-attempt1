using UnityEngine;
public class HotbarDisplay : StaticInventoryDisplay
{
    private int _maxIndexSize = 9;
    private int _currentIndex = 0;
    private float scrollDirection = 0f;

    [SerializeField] private InputReader input;
    private void Awake() // Subscribe/ unsubscribe to all events related to the hotbar
    {
        input.Hotbar1Event += Hotbar1;
        input.Hotbar2Event += Hotbar2;
        input.Hotbar3Event += Hotbar3;
        input.Hotbar4Event += Hotbar4;
        input.Hotbar5Event += Hotbar5;
        input.Hotbar6Event += Hotbar6;
        input.Hotbar7Event += Hotbar7;
        input.Hotbar8Event += Hotbar8;
        input.Hotbar9Event += Hotbar9;
        input.Hotbar10Event += Hotbar10;
        input.MouseWheelEvent += MouseWheel;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        input.Hotbar1Event -= Hotbar1;
        input.Hotbar2Event -= Hotbar2;
        input.Hotbar3Event -= Hotbar3;
        input.Hotbar4Event -= Hotbar4;
        input.Hotbar5Event -= Hotbar5;
        input.Hotbar6Event -= Hotbar6;
        input.Hotbar7Event -= Hotbar7;
        input.Hotbar8Event -= Hotbar8;
        input.Hotbar9Event -= Hotbar9;
        input.Hotbar10Event -= Hotbar10;
        input.MouseWheelEvent -= MouseWheel;
    }

    protected override void Start() // Set current index to 0 and ensure the max length is correct
    {
        base.Start();

        _currentIndex = 0;
        _maxIndexSize = slots.Length - 1;
        
        slots[_currentIndex].ToggleHighlight(); // Highlight the first slot (slot 0)
    }   
    // Set all methods related to hotbar input
    #region Hotbar Select Methods 

    private void Hotbar1()
    {
        SetIndex(0);
    }

    private void Hotbar2()
    {
        SetIndex(1);
    }
    
    private void Hotbar3()
    {
        SetIndex(2);
    }
    
    private void Hotbar4()
    {
        SetIndex(3);
    }
    
    private void Hotbar5()
    {
        SetIndex(4);
    }
    
    private void Hotbar6()
    {
        SetIndex(5);
    }
    
    private void Hotbar7()
    {
        SetIndex(6);
    }
    
    private void Hotbar8()
    {
        SetIndex(7);
    }
    
    private void Hotbar9()
    {
        SetIndex(8);
    }
    
    private void Hotbar10()
    {
        SetIndex(9);
    }

    private void MouseWheel(float scroll)
    {
        scrollDirection = scroll;
    }
    #endregion

    // Change index based on mouse scroll
    private void Update()
    {
        if (scrollDirection > 0.1f) ChangeIndex(-1);
        if (scrollDirection < -0.1f) ChangeIndex(1);
    }
    private void ChangeIndex(int direction) // Change the index based on the mousewheel
    {
        slots[_currentIndex].ToggleHighlight();
        _currentIndex += direction;

        if (_currentIndex > _maxIndexSize) _currentIndex = 0;
        if (_currentIndex < 0) _currentIndex = _maxIndexSize;
        
        slots[_currentIndex].ToggleHighlight();
    }

    private void SetIndex(int newIndex) // Change the index based on keyboard input
    {
        slots[_currentIndex].ToggleHighlight();
        if (newIndex < 0) _currentIndex = 0;
        if (newIndex > _maxIndexSize) newIndex = _maxIndexSize;
        
        _currentIndex = newIndex;
        slots[_currentIndex].ToggleHighlight();
    }

    public ItemData CurrentItem()
    {
        return slots[_currentIndex].AssignedInventorySlot.ItemData;
    }

    public InventorySlot CurrentSlot()
    {
        return slots[_currentIndex].AssignedInventorySlot;
    }

}
