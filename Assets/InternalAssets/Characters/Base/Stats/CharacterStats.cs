using System;
using TheRat.Characters.Stats;

namespace SouthBasement.Characters
{
    [Serializable]
    public sealed class CharacterStats
    {
        public readonly CharacterAttackStats AttackStats = new();
        public readonly CharacterHealthStats HealthStats = new();
        public readonly CharacterStaminaStats StaminaStats = new();
        public readonly CharacterMoveStats MoveStats = new();
    }
}