using System.Collections;
using SouthBasement.Infrastucture;
using SouthBasement.Characters.Stats;
using UnityEngine;

namespace SouthBasement.Characters
{
    public sealed class StaminaController
    {
        private CharacterStats _characterStats;
        private Coroutine _increaseCoroutine;
        private ICoroutineRunner _coroutineRunner;

        public float CurrentStamina => _characterStats.StaminaStats.Stamina.Value;
        
        public StaminaController(CharacterStats staminaStats, ICoroutineRunner coroutineRunner)
        {
            _characterStats = staminaStats;
            _coroutineRunner = coroutineRunner;
        }
        
        public bool TryDo(float staminaRequire)
        {
            if (_characterStats.StaminaStats.Stamina.Value >= staminaRequire)
            {
                _characterStats.StaminaStats.Stamina.Value -= staminaRequire;
                
                if(_increaseCoroutine != null)
                    _coroutineRunner.Stop(_increaseCoroutine);
                
                _increaseCoroutine = _coroutineRunner.Run(IncreaseStamina());
                return true;
            }

            return false;
        }

        private IEnumerator IncreaseStamina()
        {
            while (_characterStats.StaminaStats.Stamina.Value < _characterStats.StaminaStats.MaximumStamina.Value)
            {
                _characterStats.StaminaStats.Stamina.Value += 1;
                yield return new WaitForSeconds(_characterStats.StaminaStats.StaminaIncreaseRate);
            }
        }
    }
}