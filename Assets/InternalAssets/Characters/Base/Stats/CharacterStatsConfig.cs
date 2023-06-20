using System;
using SouthBasement.Characters;
using SouthBasement.Weapons;
using TheRat.Characters.Stats;
using UnityEngine;

namespace SouthBasement
{
    [Serializable]
    public sealed class CharacterStatsConfig
    {
        public CharacterAttackStats AttackStats = new();
        public CharacterHealthStats HealthStats = new();
        public CharacterStaminaStats StaminaStats = new();
        public CharacterMoveStats MoveStats = new();
    }
}