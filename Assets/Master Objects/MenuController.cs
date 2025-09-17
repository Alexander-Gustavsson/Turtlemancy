using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject Credits;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCredits()
    {
        Credits.SetActive(true);
    }

    public void HideCredits()
    {
        Credits.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
