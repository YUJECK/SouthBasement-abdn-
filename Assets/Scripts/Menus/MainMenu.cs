using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject gratitudeMenu;
    public void PlayGame(){SceneManager.LoadScene("FirstLevelBasement");}
    public void QuitGame(){Application.Quit();}
    public void GratitudeMenu(bool active){gratitudeMenu.SetActive(active);}
}
