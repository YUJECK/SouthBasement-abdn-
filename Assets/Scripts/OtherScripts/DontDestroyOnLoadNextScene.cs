using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadNextScene : MonoBehaviour
{
    public bool destroyingOnMenus = true;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded()
    {
        if(destroyingOnMenus)
        {
            Scene introIndx = SceneManager.GetSceneByBuildIndex(0);
            Scene mainMenuIndx = SceneManager.GetSceneByBuildIndex(1);
            
            if(introIndx.buildIndex == SceneManager.GetActiveScene().buildIndex)
                Destroy(gameObject);
            if(mainMenuIndx.buildIndex == SceneManager.GetActiveScene().buildIndex)
                Destroy(gameObject);
        }
    }
}
