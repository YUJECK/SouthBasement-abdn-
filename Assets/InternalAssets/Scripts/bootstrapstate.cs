using UnityEngine;
using UnityEngine.SceneManagement;

public class bootstrapstate : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(1);
    }
}