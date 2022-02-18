using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public bool isPause = false;
    public static PauseMenu instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {   
            if(isPause)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        isPause = true;
        Time.timeScale = 0f;
        menu.SetActive(true);
    }

    public void Resume()
    {
        isPause = false;
        menu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene("FirstLevelBasement");
    }
}
