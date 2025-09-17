using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpBoost;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rgbd = other.gameObject.GetComponent<Rigidbody2D>();

            rgbd.linearVelocityY = 9;

            GetComponent<Animator>().SetTrigger("Motion");
        }
    }
}
