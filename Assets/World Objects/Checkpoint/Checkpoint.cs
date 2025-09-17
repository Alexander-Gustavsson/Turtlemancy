using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("RaiseFlag");

            other.gameObject.GetComponent<PlayerMovement>().respawnPosition = transform.position;
        }
    }
}
