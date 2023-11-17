using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float rayDistance = 1f;
    [SerializeField] private InputReader input;
    public bool IsInteracting { get; private set; }

    public GameObject rayObject;

    public LayerMask contactFilter;
    public RaycastHit2D raycastHit;
    private Vector2 raycastDirection;
    private Vector2 moveDirection;

    private bool Interacted;

    private void Awake()
    {
        input.MoveEvent += HandleMove;
        input.InteractEvent += Raycast;
        input.ContinueInteractionEvent += Raycast;
    }
    private void OnDisable()
    {
        input.MoveEvent -= HandleMove;
        input.InteractEvent -= Raycast;
        input.ContinueInteractionEvent -= Raycast;
    }
    private void Raycast()
    {
        MoveDirection();
        PerformRaycast();
        InteractWithInteractable();
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
        if (raycastHit == true)
        {
            Vector3 objPosition = raycastHit.transform.position;       
        }
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
