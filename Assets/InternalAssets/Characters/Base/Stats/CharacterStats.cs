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
            
            CombatStats = _resetConfig.CombatStats;
            HealthStats = _resetConfig.HealthStats;
            StaminaStats = _resetConfig.StaminaStats;
            MoveStats = _resetConfig.MoveStats;
        }

        public void Reset()
        {
            CombatStats = _resetConfig.CombatStats;
            HealthStats = _resetConfig.HealthStats;
            StaminaStats = _resetConfig.StaminaStats;
            MoveStats = _resetConfig.MoveStats;
        }
    }
}