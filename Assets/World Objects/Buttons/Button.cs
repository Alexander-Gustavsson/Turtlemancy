using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject dust;
    [SerializeField] private GameObject target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target.GetComponent<DrawbridgeScript>().Trigger();

            transform.position = transform.position + Vector3.down * 0.1f;

            Instantiate(dust,transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            transform.position = transform.position + Vector3.up * 0.1f;
        }
    }
}
