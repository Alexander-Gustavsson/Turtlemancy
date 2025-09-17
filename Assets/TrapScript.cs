using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField] private float knockback;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(1);

            Vector2 angle = (new Vector2(collision.transform.position.x, collision.transform.position.y) - GetComponent<Collider2D>().ClosestPoint(collision.transform.position)).normalized;

            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = angle * knockback;
        }
    }
}
