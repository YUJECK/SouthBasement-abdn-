using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart_Menu : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("FirstLevelBasement");
    }
    public void HomeButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
