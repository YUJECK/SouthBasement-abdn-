using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class FirstLevelLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(1);
    }
}