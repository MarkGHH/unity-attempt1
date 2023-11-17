using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float rayDistance = 1f;
    public GameObject rayObject;
    public LayerMask contactFilter;
    public RaycastHit2D raycastHit;
    private Vector2 raycastDirection;
    public bool IsInteracting {  get; private set; }
    [SerializeField] private InputReader input;
    private Vector2 moveDirection;
    private bool Interacted;

    private void Awake()
    {
        input.MoveEvent += HandleMove;
        input.InteractEvent += Raycast;
        input.InteractCancelledEvent += HandleInteractCancelled;
    }
    private void Raycast()
    {
        MoveDirection();
        PerformRaycast();
        InteractWithInteractable();
    }
    private void HandleInteract()
    {
        IsInteracting = true;
    }
    private void HandleInteractCancelled()
    {
        IsInteracting = false;
    }
    private void HandleMove(Vector2 dir)
    {
        moveDirection = dir;
    }
    private void MoveDirection()
    {
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
    }
    private void PerformRaycast()
    {
        raycastHit = Physics2D.Raycast(rayObject.transform.position, raycastDirection, rayDistance, contactFilter);
    }
    private void Update()
    {
        MoveDirection();
        Physics2D.Raycast(rayObject.transform.position, raycastDirection, rayDistance, contactFilter);
        Debug.DrawRay(rayObject.transform.position, raycastDirection * rayDistance, Color.red);
    }
    private void InteractWithInteractable()
    {
        if (raycastHit.collider != null)
        {
            var interactable = raycastHit.collider.GetComponent<IInteract>();
            if (interactable != null && Interacted == false)
            {
                Interacted = true;
                interactable.Interact(this);
                Interacted = false;
            }
        }
    }
}
