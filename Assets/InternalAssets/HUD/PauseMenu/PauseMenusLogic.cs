using UnityEngine;

namespace SouthBasement
{
    public sealed class PauseMenusLogic : MonoBehaviour
    {
        [SerializeField] private GameObject masterMenu;
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject settingsMenu;

        public bool CurrentPauseMenuOpened => masterMenu.activeSelf;
        public bool MainMenuOpened => mainMenu.activeSelf;
        public bool SettingsOpened => settingsMenu.activeSelf;
        
        public void OpenPauseMenu()
        {
            masterMenu.SetActive(true);
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }

        public void ClosePauseMenu()
        {
            masterMenu.SetActive(false);
        }

        public void OpenSettings()
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
    }
}