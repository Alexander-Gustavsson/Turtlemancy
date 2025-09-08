using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;

    private void Start()
    {
        spawnPosition = GameObject.Find("Spawn Position").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.position = spawnPosition.position;
        }
    }
}
