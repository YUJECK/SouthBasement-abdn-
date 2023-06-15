using System.Collections;
using TheRat.Infrastucture;
using UnityEngine;
using Zenject;

namespace TheRat.Characters
{
    public sealed class StaminaController
    {
        private CharacterStats _characterStats;
        private Coroutine _increaseCoroutine;
        private ICoroutineRunner _coroutineRunner;

        public StaminaController(CharacterStats characterStats, ICoroutineRunner coroutineRunner)
        {
            _characterStats = characterStats;
            _coroutineRunner = coroutineRunner;
        }
        
        public bool TryDo(int staminaRequire)
        {
            if (_characterStats.Stamina.Value >= staminaRequire)
            {
                _characterStats.Stamina.Value -= staminaRequire;
                
                if(_increaseCoroutine != null)
                    _coroutineRunner.Stop(_increaseCoroutine);
                
                _increaseCoroutine = _coroutineRunner.Run(IncreaseStamina());
                return true;
            }

            return false;
        }

        private IEnumerator IncreaseStamina()
        {
            while (_characterStats.Stamina.Value < _characterStats.MaximumStamina)
            {
                _characterStats.Stamina.Value += 1;
                yield return new WaitForSeconds(_characterStats.StaminaIncreaseRate);
            }
        }
    }
}