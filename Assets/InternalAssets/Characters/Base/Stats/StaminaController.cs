using System.Collections;
using SouthBasement.Infrastucture;
using TheRat.Characters.Stats;
using UnityEngine;

namespace SouthBasement.Characters
{
    public sealed class StaminaController
    {
        private CharacterStaminaStats _characterStats;
        private Coroutine _increaseCoroutine;
        private ICoroutineRunner _coroutineRunner;

        public StaminaController(CharacterStaminaStats staminaStats, ICoroutineRunner coroutineRunner)
        {
            _characterStats = staminaStats;
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
            while (_characterStats.Stamina.Value < _characterStats.MaximumStamina.Value)
            {
                _characterStats.Stamina.Value += 1;
                yield return new WaitForSeconds(_characterStats.StaminaIncreaseRate);
            }
        }
    }
}