using UnityEngine;
using System.Linq;

public class RockMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float bounceForce;
    [SerializeField] private float knockback;
    [SerializeField] private float stunOnHit;
    [SerializeField] private Animator anim;
    private SpriteRenderer sprite;
    private readonly string[] reverserTags = {"EnemyBlocker", "Enemy"};

    [SerializeField] private GameObject blood;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (reverserTags.Contains(other.gameObject.tag))
        {
            ReverseMovement();
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerCollision(other.gameObject);
        }
    }

    private void ReverseMovement()
    {
        moveSpeed = -moveSpeed;
        sprite.flipX = !sprite.flipX;
    }

    private void PlayerCollision(GameObject player)
    {
        PlayerMovement playerScript = player.gameObject.GetComponent<PlayerMovement>();
        playerScript.TakeDamage();
        playerScript.TakeKnockback((player.transform.position - transform.position).normalized * knockback);

        ReverseMovement();
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocityY = 6;

            anim.SetTrigger("Hit");

            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            moveSpeed = 0;

            Instantiate(blood, transform.position, Quaternion.identity);

            Invoke("TurnOff", 0.6f);
        }
    }

    private void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
