using NTC.GlobalStateMachine;
using SouthBasement.InputServices;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [RequireComponent(typeof(PauseMenusLogic))]
    public class PauseLogic : MonoBehaviour
    {
        private PauseMenusLogic _pauseMenusLogic;
        private IInputService _inputService;
        private AudioMixersService _audioMixersService;
        private GameState _previousState;

        [Inject]
        private void Construct(IInputService inputService, AudioMixersService audioMixersService)
        {
            _inputService = inputService;
            _audioMixersService = audioMixersService;
        }

        private void Awake()
        {
            _pauseMenusLogic = GetComponent<PauseMenusLogic>();
            _inputService.OnPaused += OnPaused;
            
            Unpause();;
            _pauseMenusLogic.ClosePauseMenu();
        }

        private void OnDestroy() 
            => _inputService.OnPaused -= OnPaused;

        private void OnPaused()
        {
            if (_pauseMenusLogic.CurrentPauseMenuOpened)
            {
                Unpause();
                _pauseMenusLogic.ClosePauseMenu();
            }
            else
            {
                Pause();
                _pauseMenusLogic.OpenPauseMenu();
            }
        }

        public void Pause()
        {
            Time.timeScale = 0f;

            _previousState = GlobalStateMachine.LastGameState;
            GlobalStateMachine.Push<PausedState>();
        }

        public void Unpause()
        {
            Time.timeScale = 1f;

            if (_previousState != null)
                GlobalStateMachine.Push(_previousState.State());
        }
    }
}
