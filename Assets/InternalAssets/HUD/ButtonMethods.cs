using UnityEngine;
using UnityEngine.SceneManagement;

namespace AutumnForest
{
    public sealed class ButtonMethods : MonoBehaviour
    {
        public void SwitchScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
        public void Exit() => Application.Quit();
        public void OpenLink(string url) => Application.OpenURL(url);
        public void Enable(GameObject gameObject) => gameObject.SetActive(true);
        public void EnableDisable(GameObject gameObject) => gameObject.SetActive(!gameObject.activeSelf);
        public void Disable(GameObject gameObject) => gameObject.SetActive(false);
    }
}