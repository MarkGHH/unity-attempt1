using System.Numerics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float walkSpeed = 4.0f;
    [SerializeField] public float runSpeed = 8.0f;
    public float speed;
    public Rigidbody2D rb;
    public Animator animator;
    private SpriteRenderer sprite;
    [SerializeField] private float horizontal;
    [SerializeField] private float vertical;
    public bool isWalking;
    public bool isRunning;
    public bool isIdle;


    private void Start()
    {
        //GetComponent instantiate
        sprite = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        //Is the player moving based on X/Y input
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            //Set animator parameters based on current speed
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
            animator.SetFloat("Speed", speed);

            //Handle direction swap in animator
            if (horizontal > 0f)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
        else
        {
            isIdle = true;
        }

        //If Shift (run) is being pressed, movement speed is doubled
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            isRunning = true;
        }
        else
        {
            speed = walkSpeed;
            isWalking = true;
        }
        rb.velocity = new UnityEngine.Vector2(horizontal * speed, vertical * speed);
        speed = rb.velocity.magnitude;
    }
    //change speed parameter to isMoving, isWalking, isIdle 
    //still use speed as velocity
    //set proper animations based on speed
}
