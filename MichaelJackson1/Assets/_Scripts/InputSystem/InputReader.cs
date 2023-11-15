using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, PlayerInputActions.IGameplayActions, PlayerInputActions.IUIActions, PlayerInputActions.IBuildingActions, PlayerInputActions.IInteractingActions
{
    private PlayerInputActions playerInputActions;


    private void OnEnable() // If there is no action mapping, get the action mapping and switch to the gameplay mapping
    {
        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();

            playerInputActions.Gameplay.SetCallbacks(this);
            playerInputActions.UI.SetCallbacks(this);
            playerInputActions.Building.SetCallbacks(this);
            playerInputActions.Interacting.SetCallbacks(this);

            SetGameplay();
        }
    }

    public void SetGameplay() // Switches to the Gameplay mapping
    {
        playerInputActions.Gameplay.Enable();
        playerInputActions.UI.Disable();
    }

    public void SetUI() // Switches to the UI mapping
    {
        playerInputActions.UI.Enable();
        playerInputActions.Gameplay.Disable();
    }

    public void SetInteracting()
    {
        playerInputActions.Interacting.Enable();
        playerInputActions.Gameplay.Disable();
    }

    public void SetBuilding() 
    {
        playerInputActions.Building.Enable();
        playerInputActions.Gameplay.Disable();
    }

    // All necessary events defined below to handle input actions
    #region Gameplay
    public event Action<Vector2> MoveEvent;

    public event Action RunEvent;
    public event Action RunCancelledEvent;

    public event Action DashEvent;
    public event Action DashCancelledEvent;

    public event Action InteractEvent;
    public event Action InteractCancelledEvent;

    public event Action UIModeEvent;

    public event Action BackpackEvent;

    public event Action Hotbar1Event;
    public event Action Hotbar2Event;
    public event Action Hotbar3Event;
    public event Action Hotbar4Event;
    public event Action Hotbar5Event;
    public event Action Hotbar6Event;
    public event Action Hotbar7Event;
    public event Action Hotbar8Event;
    public event Action Hotbar9Event;
    public event Action Hotbar10Event;

    public event Action<float> MouseWheelEvent;
    public event Action<Vector2> MousePositionEvent;
    public event Action<bool> PerformActionEvent;
    public event Action<bool> CancelActionEvent;
    public event Action BuildingModeEvent;
    #endregion

    #region UI
    public event Action ExitUIEvent;
    #endregion

    #region Interacting
    public event Action ContinueInteractionEvent;
    public event Action ExitInteractionEvent;
    #endregion

    #region Building
    public event Action NextItemEvent;
    public event Action PreviousItemEvent;
    public event Action ExitBuildingEvent;
    public event Action<Vector2> MousePositionBuildingEvent;
    #endregion


    // Sends an event to all listeners when On.. is triggered through the PlayerInputActions script
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            RunEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            RunCancelledEvent?.Invoke();
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            DashEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            DashCancelledEvent?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InteractEvent?.Invoke();
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            InteractCancelledEvent?.Invoke();
        }
    }

    public void OnUIMode(InputAction.CallbackContext context) // Switch to UI map!
    {
        if (context.phase == InputActionPhase.Performed)
        {
            UIModeEvent?.Invoke();
            SetUI();
        }
    }

    public void OnExitUI(InputAction.CallbackContext context) // Switch to Gameplay map!
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ExitUIEvent?.Invoke();
            SetGameplay();
        }
    }
    public void OnHotbar1(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Hotbar1Event?.Invoke();
        }
    }

    public void OnHotbar2(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Hotbar2Event?.Invoke();
        }
    }

    public void OnHotbar3(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Hotbar3Event?.Invoke();
        }
    }

    public void OnHotbar4(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Hotbar4Event?.Invoke();
        }
    }

    public void OnHotbar5(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Hotbar5Event?.Invoke();
        }
    }
    public void OnHotbar6(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Hotbar6Event?.Invoke();
        }
    }

    public void OnHotbar7(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Hotbar7Event?.Invoke();
        }
    }

    public void OnHotbar8(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Hotbar8Event?.Invoke();
        }
    }

    public void OnHotbar9(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Hotbar9Event?.Invoke();
        }
    }

    public void OnHotbar10(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Hotbar10Event?.Invoke();
        }
    }

    public void OnMouseWheel(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            MouseWheelEvent?.Invoke(context.ReadValue<float>());
        }
    }

    public void OnPerformAction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PerformActionEvent?.Invoke(true);
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            PerformActionEvent?.Invoke(false);
        }
    }
    public void OnCancelAction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            CancelActionEvent?.Invoke(true);
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            CancelActionEvent?.Invoke(false);
        }
    }
    public void OnMousePosition(InputAction.CallbackContext context)
    {
        MousePositionEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMousePositionBuilding(InputAction.CallbackContext context)
    {
        MousePositionBuildingEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnContinueinteraction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ContinueInteractionEvent?.Invoke();
        }
    }

    public void OnBuildingMode(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            BuildingModeEvent?.Invoke();
            SetBuilding();
        }
    }

    public void OnNextItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            NextItemEvent?.Invoke();
        }
    }

    public void OnPreviousItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PreviousItemEvent?.Invoke();
        }
    }

    public void OnExitBuilding(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ExitBuildingEvent?.Invoke();
            SetGameplay();
        }
    }

    public void OnExitInteraction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ExitInteractionEvent?.Invoke();
            SetGameplay();
        }
    }

    public void OnBackpack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            BackpackEvent?.Invoke();
        }
    }
}
