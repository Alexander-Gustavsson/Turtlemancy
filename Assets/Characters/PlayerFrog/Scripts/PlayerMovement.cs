using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerAcceleration;
    [SerializeField] private float jumpForce;
    [SerializeField] private int maxHealth;
    [SerializeField] public int appleCounter;
    public List<GameObject> collectedApples;
    public List<GameObject> enemiesKilled;

    public Vector3 respawnPosition;
    [SerializeField] private float groundRayDistance;
    [SerializeField] private Color greenHealth, redHealth;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip[] jumpSound;
    [SerializeField] private float audioScaler;
    [SerializeField] private GameObject applesParticle;
    [SerializeField] private GameObject jumpParticle;

    private Rigidbody2D body; // References
    private Slider healthSlider;
    private SpriteRenderer spriteRenderer;
    private LayerMask groundLayer;
    private Image healthBarFill;
    private TMP_Text appleText;
    private Transform leftFoot;
    private Transform rightFoot;
    private Animator anim;
    private AudioSource audioPlayer;
    private CameraScript cameraScript;

    private float horizontalInput; 
    private float verticalInput;
    private bool isGrounded;
    public bool isStunned = false;
    [SerializeField] private int currentHealth;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthSlider = GameObject.Find("Canvas").transform.Find("Slider_Health").gameObject.GetComponent<Slider>();
        healthBarFill = healthSlider.transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
        appleText = GameObject.Find("Canvas").transform.Find("Text_Apples").GetComponent<TMP_Text>();
        groundLayer = LayerMask.GetMask("Ground", "Platform");
        leftFoot = transform.Find("LeftFoot");
        rightFoot = transform.Find("RightFoot");
        anim = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        respawnPosition = transform.position;

        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isGrounded = CheckGrounded();
        float horizontalVelocity = body.linearVelocityX;

        anim.SetFloat("Horizontal Velocity", Mathf.Abs(body.linearVelocityX));
        anim.SetFloat("Vertical Velocity", body.linearVelocityY);
        anim.SetBool("Is Grounded", isGrounded);

        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            cameraScript.offset = new Vector3(-Mathf.Abs(cameraScript.maxOffset.x), 0, -10);
        } else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            cameraScript.offset = new Vector3(Mathf.Abs(cameraScript.maxOffset.x), 0, -10);
        }

        if (verticalInput < 0)
        {
            cameraScript.offset.y = -cameraScript.maxOffset.y;
        }
        else if (verticalInput > 0)
        {
            cameraScript.offset.y = cameraScript.maxOffset.y;
        } else
        {
            cameraScript.offset.y = 0;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        if (isStunned)
        {
            return;
        }

        float newVelocityX = horizontalInput * playerSpeed;
        body.linearVelocityX = newVelocityX;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Apple"))
        {
            appleCounter++;
            UpdateAppleText();
            collectedApples.Add(other.gameObject);
            other.gameObject.SetActive(false);

            Instantiate(applesParticle, other.transform.position, Quaternion.identity);

            audioPlayer.pitch = Random.Range(0.8f, 1.2f);
            audioPlayer.PlayOneShot(pickupSound, audioScaler);
        }


        if (other.CompareTag("HealthPickup") && currentHealth < maxHealth)
        {
            int restoredHealth = other.GetComponent<HealthPickup>().health;
            currentHealth += restoredHealth;
            UpdateHealthBar();
            other.gameObject.SetActive(false);

            audioPlayer.pitch = Random.Range(0.8f, 1.2f);
            audioPlayer.PlayOneShot(pickupSound, audioScaler);

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }

    private void UpdateAppleText()
    {
        appleText.text = "Apples: " + appleCounter;
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
    
    public void TakeDamage()
    {
        TakeDamage(1);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        transform.position = respawnPosition;
        currentHealth = maxHealth;
        body.linearVelocity = Vector2.zero;
        cameraScript.ZoomIn(transform, true);
        UpdateHealthBar();

        foreach (Transform child in GameObject.Find("Apples").transform)
        {
            if (child.gameObject.CompareTag("Apple"))
            {
                child.gameObject.SetActive(true);
            }
        }

        foreach (GameObject enemy in enemiesKilled)
        {
            enemy.SetActive(true);
        }

        appleCounter = 0;
        UpdateAppleText();
        
    }

    private void Jump()
    {
        body.AddForceY(jumpForce, ForceMode2D.Impulse);

        audioPlayer.pitch = Random.Range(0.8f, 1.2f);
        audioPlayer.PlayOneShot(jumpSound[Random.Range(0, 3)], audioScaler);

        Instantiate(jumpParticle, transform.position, Quaternion.identity);
    }
    public void TakeKnockback(Vector2 knockbackForce)
    {
        isStunned = true;
        body.AddForce(knockbackForce, ForceMode2D.Impulse);
        Invoke("CleanseStun", 0.2f);
    }

    private void CleanseStun()
    {
        isStunned = false;
    }


    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;

        if (currentHealth <= 2)
        {
            healthBarFill.color = redHealth;
        } else
        {
            healthBarFill.color = greenHealth;
        }
    }
}