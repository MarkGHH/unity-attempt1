using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    //Movement and animations
    [SerializeField] private float walkSpeed = 4.0f;
    [SerializeField] private float runSpeed = 8.0f;
    private float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private Vector2 playerinput;

    private void Start()
    {
        //GetComponent instantiate
        sprite = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(this.gameObject);
    }
    private void FixedUpdate()
    {
        //Is the player moving based on X/Y input
        playerinput.x = Input.GetAxisRaw("Horizontal");
        playerinput.y = Input.GetAxisRaw("Vertical");
        PlayerAnimation();

        //Speed calculation including normalization of speed through ClampMagnitude (ensures the speed isn't increased when moving diagonally)
        //Pass the speed value outside of if() statements to ensure speed is always updated -> this ensures that the right animation is selected (idle/walk/run), the direction should not be touched in case the player is not trying to move
        playerinput = Vector2.ClampMagnitude(playerinput, 1f);
        rb.velocity = new Vector2(playerinput.x * speed, playerinput.y * speed);
        speed = rb.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    private void PlayerAnimation()
    {
        if (playerinput.x != 0 || playerinput.y != 0) //Is player moving?
        {
            //Pass movement to animator
            animator.SetFloat("Horizontal", playerinput.x);
            animator.SetFloat("Vertical", playerinput.y);

            //Flip sprite based on input
            if (playerinput.x > 0f)
            {
                sprite.flipX = true;
            }
            else sprite.flipX = false;
        }
        //If not moving, speed 0
        else speed = 0f;

        //If shift, use runspeed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else speed = walkSpeed;        
    }
}
