using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("FirstLevelBasement");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
