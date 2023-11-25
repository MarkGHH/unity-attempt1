using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 0f;
    private readonly float walkSpeed = 4.0f;
    private readonly float runSpeed = 6.0f;
    private readonly float dashingPower = 17f;
    private readonly float dashingTime = 0.2f;
    private readonly float dashingCooldown = 1f;

    private bool isRunning;
    private bool isDashing;
    private bool canDash = true;

    public Vector2 moveDirection;

    private TrailRenderer tr;
    public Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    [SerializeField] public InputReader input;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();

        // Subcribe to all the events from the InputReader related to the player and start the function on receive
        input.MoveEvent += HandleMove;
        input.RunEvent += HandleRun;
        input.RunCancelledEvent += HandleRunCancelled;
        input.DashEvent += HandleDash;
    }

    private void FixedUpdate()
    {
        Move();
        StartCoroutine(Dash());
    }

    // Set bools depending on the events received
    private void HandleMove(Vector2 dir)
    {
        moveDirection = dir;
    }
    private void HandleRun()
    {
        isRunning = true;
    }
    private void HandleRunCancelled()
    {
        isRunning = false;
    }
    private void HandleDash()
    {
        if (moveDirection != Vector2.zero & !isDashing)
        {
            isDashing = true;
            AudioManager.Instance.PlaySFX("Dash");
        }
    }
    private void Move() // General player movement
    {
        if (moveDirection != Vector2.zero) // If the Vector2 is not equal to (0, 0) then 
        {
            if (isRunning)
            {
                speed = runSpeed;
            }
            else speed = walkSpeed;

        }
        else speed = 0f;

        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);

        Animate();
    }
    private IEnumerator Dash()
    {
        if (isDashing && canDash)
        {
            rb.velocity = new Vector2(moveDirection.x * dashingPower, moveDirection.y * dashingPower);
            tr.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            tr.emitting = false;
            canDash = false;
            yield return new WaitForSeconds(dashingCooldown);
            isDashing = false;
            canDash = true;
        }
    }

    private void Animate()
    {
        animator.SetFloat("Speed", speed);
        if (moveDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);

            if (moveDirection.x > 0f)
            {
                sprite.flipX = true;
            }
            else sprite.flipX = false;
        }
    }
}
