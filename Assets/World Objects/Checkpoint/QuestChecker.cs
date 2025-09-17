using NUnit.Framework;
using UnityEngine;

public class QuestChecker : MonoBehaviour
{
    [SerializeField] private GameObject textBox, finishedText, unfinishedText;
    private GameObject activeText;
    [SerializeField] private CameraScript cameraScript;

    [SerializeField] private int QuestGoal = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerScript = other.gameObject.GetComponent<PlayerMovement>();

            QuestGoal -= playerScript.collectedApples.Count;

            foreach (GameObject apple in playerScript.collectedApples)
            {
                Destroy(apple);
            }
            playerScript.collectedApples.Clear();

            if (playerScript.collectedApples.Count >= QuestGoal)
            {
                textBox.SetActive(true);
                finishedText.SetActive(true);
                activeText = finishedText;

                try {
                    GameObject.Find("QuestGiver").SetActive(false);
                } catch
                {
                    print("*The player walks into the textbox again, like a bufoon*");
                }                
                GameObject.Find("Keyholder").transform.Find("Key").gameObject.SetActive(true); // Enklare än att använda FindObjectsByType. GO.Find() funkar inte på inactives.

            } else
            {
                textBox.SetActive(true);
                unfinishedText.SetActive(true);
                activeText = unfinishedText;
            }

            cameraScript.ZoomIn(textBox.transform);
            cameraScript.maxOffset = Vector3.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textBox.SetActive(false);
            activeText.SetActive(false);
            cameraScript.ReturnToPlayer();
        }
    }
}