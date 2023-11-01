using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 0f;
    private float walkSpeed = 4.0f;
    private float sprintSpeed = 8.0f;
    private bool isSprinting;
    private bool isWalking;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private PlayerInputActions playerInputActions;

    public Vector2 Vector2;



    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Run.performed += x => isSprinting = true;
        playerInputActions.Player.Run.canceled += x => isSprinting = false;
        playerInputActions.Player.Move.performed += x => isWalking = true;
        playerInputActions.Player.Move.canceled += x => isWalking = false;
    }
    private void FixedUpdate()
    {
        Vector2 moveDirection = playerInputActions.Player.Move.ReadValue<Vector2>();
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);

        if (moveDirection.x != 0f || moveDirection.y != 0f)
        {
            if (isSprinting)
            {
                speed = sprintSpeed;
            }
            else if (isWalking)
            {
                speed = walkSpeed;
            }
            else speed = 0f;

            if (moveDirection.x > 0f)
            {
                sprite.flipX = true;
            }
            else sprite.flipX = false;
        }
        else speed = 0f;

        animator.SetFloat("Speed", speed);

        if (moveDirection.x != 0f || moveDirection.y != 0f)
        {
            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);
        }
        Vector2 = new Vector2(moveDirection.x, moveDirection.y);
    }
    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    public void OnDisable()
    {
        playerInputActions.Disable();
    }
}

