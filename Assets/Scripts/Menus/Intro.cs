using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public float waitTime = 10f;

    private void Update()
    {
        StartCoroutine(CloseIntro());
    }
    IEnumerator CloseIntro()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(1);
    }
}
