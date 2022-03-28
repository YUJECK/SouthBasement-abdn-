using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private string nextLevelName = "FirstLevelBasement";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
            SceneManager.LoadScene(nextLevelName);
    }
}
