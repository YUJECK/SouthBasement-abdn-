using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public float waitTime = 10f;

    private void Start()
    {
        StartCoroutine(CloseIntro());
    }
    IEnumerator CloseIntro()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(1);
    }
}
