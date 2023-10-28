using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float rayDistance = 1f;
    public GameObject rayObject;
    public LayerMask contactFilter;
    public RaycastHit2D raycastHit;
    private Vector2 raycastDirection;
    private Vector2 playerinput;
    private readonly Collider[] colliders = new Collider[3];


    private void FixedUpdate()
    {
        //Raycast direction -> this should replace the direction input (transform.right) using -transform.right/transform.right/-transform.up/transform.up based on which direction the character is facing

        playerinput.x = Input.GetAxisRaw("Horizontal");
        playerinput.y = Input.GetAxisRaw("Vertical");

        if (playerinput.x != 0 || playerinput.y != 0)
        {
            if (playerinput.x == 1)
            {
                raycastDirection = transform.right;
            }
            else if (playerinput.x == -1)
            {
                raycastDirection = -transform.right;
            }
            else if (playerinput.y == 1)
            {
                raycastDirection = transform.up;
            }
            else raycastDirection = -transform.up;
        }

        //Perform the raycast
        raycastHit = Physics2D.Raycast(rayObject.transform.position, raycastDirection, rayDistance, contactFilter);

        //Draw the ray
        Debug.DrawRay(rayObject.transform.position, raycastDirection * rayDistance, Color.red);

        if (raycastHit.collider != null)
        {
            var interactable = raycastHit.collider.GetComponent<InteractInterface>();
            if (interactable != null && Input.GetKey("e"))
            {
                interactable.Interact(this);
            }
        }
    }

}
