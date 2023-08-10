using SouthBasement.InputServices;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        private IInputService _inputService;

        private bool CurrentPaused => pauseMenu.activeSelf;
        
        [Inject]
        private void Construct(IInputService inputService)
            => _inputService = inputService;

        private void Awake()
        {
            _inputService.OnPaused += OnPaused;
            
            Unpause();
        }

        private void OnDestroy() 
            => _inputService.OnPaused -= OnPaused;

        private void OnPaused()
        {
            if (CurrentPaused) Unpause();
            else Pause();
        }

        public void Pause()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        public void Unpause()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
