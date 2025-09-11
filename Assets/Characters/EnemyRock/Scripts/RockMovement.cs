using UnityEngine;
using System.Linq;

public class RockMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float bounceForce;
    private SpriteRenderer sprite;
    private readonly string[] reverserTags = {"EnemyBlocker", "Enemy"};

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
            moveSpeed = -moveSpeed;
            sprite.flipX = !sprite.flipX;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocityY = 6;
            Destroy(gameObject);
        }
    }
}
