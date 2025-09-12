using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    private CameraScript cameraScript;

    private void Start()
    {
        spawnPosition = GameObject.Find("Spawn Position").transform;
        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().KillPlayer();
        }
    }
}
