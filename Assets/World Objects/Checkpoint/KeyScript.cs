using UnityEngine;
using UnityEngine.SceneManagement;
public class KeyScript : MonoBehaviour
{
    [SerializeField] private CameraScript cameraScript;
    [SerializeField] private int levelToLoad;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameObject star = GameObject.Find("Star");
            star.GetComponent<SpriteRenderer>().enabled = true;

            cameraScript.maxOffset = Vector3.zero;
            cameraScript.ZoomIn(star.transform);

            Invoke("ExitLevel", 5f);
        }
    }

    private void ExitLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
