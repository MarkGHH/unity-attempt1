using System.Numerics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 4.0f;
    [SerializeField] private float runSpeed = 8.0f;
    private float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private UnityEngine.Vector3 playerinput;

    private void Start()
    {
        //GetComponent instantiate
        sprite = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        //Is the player moving based on X/Y input
        playerinput.x = Input.GetAxisRaw("Horizontal");
        playerinput.y = Input.GetAxisRaw("Vertical");
        PlayerAnimation();

        //Speed calculation including normalization of speed through ClampMagnitude (ensures the speed isn't increased when moving diagonally)
        playerinput = UnityEngine.Vector2.ClampMagnitude(playerinput, 1f);
        rb.velocity = new UnityEngine.Vector3(playerinput.x * speed, playerinput.y * speed, 0f);
        speed = rb.velocity.magnitude;

        //Pass the speed value outside of if() statements to ensure speed is always updated -> this ensures that the right animation is selected (idle/walk/run), the direction should not be touched in case the player is not trying to move
        //Pass speed at the end of the frame, so that the speed value of the current frame is being used
        animator.SetFloat("Speed", speed);
    }
    private void PlayerAnimation()
    {
        if (playerinput.x != 0 || playerinput.y != 0)
        {
            //Set animator parameters for horizontal and vertical, if the player is trying to move
            animator.SetFloat("Horizontal", playerinput.x);
            animator.SetFloat("Vertical", playerinput.y);

            //Handle direction swap for side animations (left v. right)
            if (playerinput.x > 0f)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }

        }
        //If the player is not trying to move, set speed to 0f to engage Idle animation
        else
        {
            speed = 0f;
        }
        //If Shift (run) is being pressed, movement speed is doubled
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }
}
