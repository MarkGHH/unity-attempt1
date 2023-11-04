using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, PlayerInputActions.IGameplayActions, PlayerInputActions.IUIActions
{
    private PlayerInputActions playerInputActions;

    private void OnEnable() // If there is no action mapping, get the action mapping and switch to the gameplay mapping
    {
        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();

            playerInputActions.Gameplay.SetCallbacks(this);
            playerInputActions.UI.SetCallbacks(this);

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
        playerInputActions.Gameplay.Disable();
        playerInputActions.UI.Enable();
    }

    // All necessary events defined below to handle input actions
    public event Action<Vector2> MoveEvent;

    public event Action RunEvent;
    public event Action RunCancelledEvent;

    public event Action DashEvent;
    public event Action DashCancelledEvent;

    public event Action InteractEvent;

    public event Action PauseEvent;
    public event Action ResumeEvent;

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
    }

    public void OnPause(InputAction.CallbackContext context) // Switch to UI map!
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            SetUI();
        }
    }

    public void OnResume(InputAction.CallbackContext context) // Switch to Gameplay map!
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            SetGameplay();
        }
    }
}
