using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;

    [SerializeField] private float groundRayDistance;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;

    private float horizontalInput;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");


        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        } else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        
        if (Input.GetButtonDown("Jump") && CheckGrounded())
        {
            print(body != null);
            body.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
    }
    void FixedUpdate()
    {
        body.linearVelocityX = horizontalInput * playerSpeed;
    }

    private bool CheckGrounded()
    {
        //Debug.DrawRay(leftFoot.position, Vector2.down * groundRayDistance, Color.red, 0.1f);

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
