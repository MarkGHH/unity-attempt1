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
    private float horizontal;
    private float vertical;

    private void Start()
    {
        //GetComponent instantiate
        sprite = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //Is the player moving based on X/Y input
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            //Set animator parameters for horizontal and vertical, if the player is trying to move
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);

            //Handle direction swap for side animations (left v. right)
            if (horizontal > 0f)
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
        rb.velocity = new UnityEngine.Vector2(horizontal * speed, vertical * speed);
        speed = rb.velocity.magnitude;

        //Pass the speed value outside of if() statements to ensure speed is always updated -> this ensures that the right animation is selected (idle/walk/run), the direction should not be touched in case the player is not trying to move
        //Pass speed at the end of the frame, so that the speed value of the current frame is being used
        animator.SetFloat("Speed", speed);
    }
}
