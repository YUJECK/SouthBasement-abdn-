using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SouthBasement
{
    public sealed class PauseMenuButtons : MonoBehaviour
    {
        [SerializeField] private PauseMenuLogic pauseMenuLogic;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject settingsMenu;
        
        private RunController _runController;

        [Inject]
        private void Construct(RunController runController)
            => _runController = runController;

        public void Resume()
        {
            pauseMenuLogic.Unpause();
        }

        public void Settings()
        {
            settingsMenu.SetActive(!settingsMenu.activeSelf);
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }

        public void ReturnToMainMenu()
        {
            _runController.EndRun();
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1f;
        }
    }
}