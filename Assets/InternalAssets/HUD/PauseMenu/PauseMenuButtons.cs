using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

namespace SouthBasement
{
    public sealed class PauseMenuButtons : MonoBehaviour
    {
        [SerializeField] private PauseLogic pauseLogic;
        [SerializeField] private PauseMenusLogic menusLogic;
        
        private RunController _runController;

        [Inject]
        private void Construct(RunController runController)
            => _runController = runController;

        public void Resume()
        {
            menusLogic.ClosePauseMenu();
            pauseLogic.Unpause();
        }

        public void Settings()
        {
            if(menusLogic.MainMenuOpened) menusLogic.OpenSettings();
            else menusLogic.OpenPauseMenu();
        }

        public void ReturnToMainMenu()
        {
            _runController.EndRun();
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1f;
        }
    }
}