using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float mSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        //Input sets horizontal/vertical to a value of -1.0 to 1.0, animator uses these values within the parameters to set an animation
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("mSpeed", mSpeed);
        /*  if (movement.x > 0f)
          {
              sprite.flipX = true;    
          }
          else if (movement.x < 0f) 
          {
              sprite.flipX = false; 
          }*/

    }
/*
    private void FixedUpdate()
    {
        //Movement - FixedUpdate ensures that it is unrelated to FPS and instead runs +- 50 times per second, further optimzed with deltatime
        rb.MovePosition(rb.position + movement * mSpeed * Time.fixedDeltaTime);
    }*/
}
