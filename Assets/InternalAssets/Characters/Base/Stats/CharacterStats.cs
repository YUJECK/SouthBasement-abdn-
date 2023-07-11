using System;
using SouthBasement.Characters.Stats;

namespace SouthBasement.Characters
{
    [Serializable]
    public sealed class CharacterStats
    {
        public readonly CharacterAttackStats AttackStats = new();
        public readonly CharacterHealthStats HealthStats = new();
        public readonly CharacterStaminaStats StaminaStats = new();
        public readonly CharacterMoveStats MoveStats = new();
        
        public CharacterStats(CharacterStatsConfig config)
        {
            AttackStats = config.AttackStats;
            HealthStats = config.HealthStats;
            StaminaStats = config.StaminaStats;
            MoveStats = config.MoveStats;
        }
    }
}