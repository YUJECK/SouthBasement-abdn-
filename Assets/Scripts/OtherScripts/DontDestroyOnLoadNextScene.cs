using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadNextScene : MonoBehaviour
{
    public bool destroyingOnMenus = true;
    public List<int> menusIdexes = new List<int>();
    private bool destroy = false;

    private void Start()
    { 
        DontDestroyOnLoad(gameObject);
        menusIdexes.Add(0);
        menusIdexes.Add(1);
        menusIdexes.Add(6);
    }
    private void OnLevelWasLoaded()
    {
        if(destroyingOnMenus) //Уничтожение если сцена меню
        {
            for (int i = 0; i < menusIdexes.Count; i++)
            {
                Scene activeScene = SceneManager.GetActiveScene();
                if (activeScene.buildIndex == menusIdexes[i]) Destroy(gameObject);
            }
        }
        if(destroy) Destroy(gameObject);
    }
    public void Disable(){destroy = true;} //Выключить 
    public void Enable(){destroy = false;} //Включить
}
