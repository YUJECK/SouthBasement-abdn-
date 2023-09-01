using System;
using SouthBasement.Characters.Stats;
using UnityEngine;

namespace SouthBasement.Characters
{
    [Serializable]
    public sealed class CharacterStats
    {
        public CharacterCombatStats CombatStats { get; private set; } 
        public CharacterHealthStats HealthStats { get; private set; }
        public CharacterStaminaStats StaminaStats { get; private set; }
        public CharacterMoveStats MoveStats { get; private set; }

        private CharacterStatsConfig _resetConfig;
        
        public CharacterStats(CharacterStatsConfig config)
        {
            _resetConfig = config;
            _resetConfig = ScriptableObject.Instantiate(_resetConfig);
            
            CombatStats = new CharacterCombatStats(_resetConfig.CombatStats.DefaultStats);
            HealthStats = new CharacterHealthStats();
            HealthStats.SetHealth(_resetConfig.HealthStats.CurrentHealth, _resetConfig.HealthStats.CurrentHealth);

            StaminaStats = new CharacterStaminaStats
            {
                StaminaIncreaseRate = _resetConfig.StaminaStats.StaminaIncreaseRate,
                Stamina = _resetConfig.StaminaStats.Stamina,
                MaximumStamina = _resetConfig.StaminaStats.MaximumStamina
            };

            MoveStats = new CharacterMoveStats
            {
                DashStaminaRequire = _resetConfig.MoveStats.DashStaminaRequire
            };
        }

        public void Reset()
        {
            CombatStats.SetStats(CombatStats.DefaultStats);
            HealthStats.SetHealth(_resetConfig.HealthStats.CurrentHealth, _resetConfig.HealthStats.CurrentHealth);
            
            StaminaStats.StaminaIncreaseRate = _resetConfig.StaminaStats.StaminaIncreaseRate;
            StaminaStats.Stamina = _resetConfig.StaminaStats.Stamina;
            StaminaStats.MaximumStamina = _resetConfig.StaminaStats.MaximumStamina;
            
            MoveStats.DashStaminaRequire = _resetConfig.MoveStats.DashStaminaRequire;
        }
    }
}