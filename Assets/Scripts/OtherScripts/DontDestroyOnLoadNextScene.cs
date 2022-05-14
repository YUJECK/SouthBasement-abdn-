using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadNextScene : MonoBehaviour
{
    public bool destroyingOnMenus = true;
    private bool destroy = false;

    private void Start(){ DontDestroyOnLoad(gameObject); }
    private void OnLevelWasLoaded()
    {
        if(destroyingOnMenus) //Уничтожение если сцена меню
        {
            Scene introIndx = SceneManager.GetSceneByBuildIndex(0);
            Scene mainMenuIndx = SceneManager.GetSceneByBuildIndex(1);
            
            if(introIndx.buildIndex == SceneManager.GetActiveScene().buildIndex)
                Destroy(gameObject);
            if(mainMenuIndx.buildIndex == SceneManager.GetActiveScene().buildIndex)
                Destroy(gameObject);
        }
        if(destroy) Destroy(gameObject);
    }
    public void Disable(){destroy = true;} //Выключить 
    public void Enable(){destroy = false;} //Включить
}
