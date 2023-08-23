using NTC.GlobalStateMachine;
using UnityEngine;

namespace SouthBasement
{
    public sealed class ThemePlayer : StateMachineUser
    {
        [SerializeField] private AudioSource mainTheme; 
        [SerializeField] private AudioSource fightTheme; 
        [SerializeField] private AudioSource npcTheme; 
        [SerializeField] private AudioSource deathTheme;
        [SerializeField] private AudioSource pauseTheme;

        private AudioSource _currentTheme;

        protected override void OnIdle()
        {
            if(_currentTheme != null)
                _currentTheme.Pause();
            mainTheme.Play();
            _currentTheme = mainTheme;
        }

        protected override void OnDied()
        {
            if(_currentTheme != null)
                _currentTheme.Pause();
            deathTheme.Play();
            _currentTheme = fightTheme;
        }

        protected override void OnFight()
        {
            if(_currentTheme != null)
                _currentTheme.Pause();
            fightTheme.Play();
            _currentTheme = fightTheme;
        }

        protected override void OnNPC()
        {
            if(_currentTheme != null)
                _currentTheme.Pause();
            npcTheme.Play();
            _currentTheme = npcTheme;
        }

        protected override void OnPaused()
        {
            Debug.Log("sdlkfskldflsdfkljds");
            
            if(_currentTheme != null)
                _currentTheme.Pause();
            
            pauseTheme.Play();
            _currentTheme = pauseTheme;
        }
    }
}