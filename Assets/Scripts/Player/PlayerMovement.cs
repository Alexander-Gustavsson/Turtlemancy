using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private float groundRayDistance;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private LayerMask groundLayer;
    private Transform leftFoot;
    private Transform rightFoot;
    private Animator anim;

    private float horizontalInput;
    private bool isGrounded;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("Ground");
        leftFoot = transform.Find("LeftFoot");
        rightFoot = transform.Find("RightFoot");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        isGrounded = CheckGrounded();

        anim.SetFloat("Horizontal Velocity", Mathf.Abs(body.linearVelocityX));
        anim.SetFloat("Vertical Velocity", body.linearVelocityY);
        anim.SetBool("Is Grounded", isGrounded);

        print(Mathf.Abs(body.linearVelocityY) > 0.1);

        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        } else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
    }
    void FixedUpdate()
    {
        body.linearVelocityX = horizontalInput * playerSpeed;
    }

    private bool CheckGrounded()
    {

        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, groundRayDistance, groundLayer);
        if (leftHit.collider != null)
        {
            return true;
        }

        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, groundRayDistance, groundLayer);
        if (rightHit.collider != null) 
        {
            return true;
        }

        return false;
    }
}