using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float rayDistance = 1f;
    public GameObject rayObject;
    public LayerMask contactFilter;
    public RaycastHit2D raycastHit;
    private Vector2 raycastDirection;
    private PlayerInputActions playerInputActions;
    private bool isInteracting;
    [SerializeField] private InputReader input;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Gameplay.Interact.performed += x => isInteracting = true;
        playerInputActions.Gameplay.Interact.canceled += x => isInteracting = false;

        input.InteractEvent += HandleInteract;
    }
    private void FixedUpdate()
    {
        //Raycast direction -> this should replace the direction input (transform.right) using -transform.right/transform.right/-transform.up/transform.up based on which direction the character is facing
        Vector2 moveDirection = playerInputActions.Gameplay.Move.ReadValue<Vector2>();

        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            if (moveDirection.y > 0.7f)
            {
                raycastDirection = transform.up;
            }
            else if (moveDirection.y < -0.7f)
            {
                raycastDirection = -transform.up;
            }
            else if (moveDirection.x == 1)
            {
                raycastDirection = transform.right;
            }
            else raycastDirection = -transform.right;
        }

        //Perform the raycast
        raycastHit = Physics2D.Raycast(rayObject.transform.position, raycastDirection, rayDistance, contactFilter);

        //Draw the ray
        Debug.DrawRay(rayObject.transform.position, raycastDirection * rayDistance, Color.red);

        if (raycastHit.collider != null)
        {
            var interactable = raycastHit.collider.GetComponent<InteractInterface>();
            if (interactable != null && isInteracting == true)
            {
                interactable.Interact(this);
            }
        }
    }
    private void HandleInteract()
    {
        isInteracting = true;
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }
}
