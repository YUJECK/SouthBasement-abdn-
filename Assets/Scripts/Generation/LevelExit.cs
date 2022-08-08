using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    public UnityEvent beforeEnterNextScene = new UnityEvent();

    private void EnterToTheNextLevel()
    {
        beforeEnterNextScene.Invoke();
        SceneManager.LoadScene(nextLevelName);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
            EnterToTheNextLevel();
    }
}