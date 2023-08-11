using SouthBasement.InputServices;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace SouthBasement
{
    public class PauseMenuLogic : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        
        private IInputService _inputService;
        private AudioMixersService _audioMixersService;

        private bool CurrentPaused => pauseMenu.activeSelf;
        
        [Inject]
        private void Construct(IInputService inputService, AudioMixersService audioMixersService)
        {
            _inputService = inputService;
            _audioMixersService = audioMixersService;
        }

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
            
            _audioMixersService.PauseAllAudio();
        }

        public void Unpause()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            
            _audioMixersService.UppauseAllAudio();
        }
    }
}
