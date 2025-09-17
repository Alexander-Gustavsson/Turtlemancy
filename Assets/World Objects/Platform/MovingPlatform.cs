using Unity.Properties;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform target1, target2;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private float moveSpeed;
    void Start()
    {
        currentTarget = target1;
    }

    void FixedUpdate() 
    {
        if(transform.position == target1.position)
        {
            currentTarget = target2;
        } else if (transform.position == target2.position)
        {
            currentTarget = target1;
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && other.transform.position.y > transform.position.y)
        {

            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}