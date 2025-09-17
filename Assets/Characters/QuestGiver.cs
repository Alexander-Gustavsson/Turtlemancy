using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private CameraScript cameraScript;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            cameraScript.ZoomIn(textBox.transform);
            cameraScript.maxOffset = Vector3.zero;

            textBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            textBox.SetActive(false);
            cameraScript.ReturnToPlayer();
        }
    }
}
