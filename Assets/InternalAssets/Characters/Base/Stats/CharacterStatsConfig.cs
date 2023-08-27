using System;
using SouthBasement.Characters;
using SouthBasement.Weapons;
using SouthBasement.Characters.Stats;
using UnityEngine;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Infrastructure+"CharacterStatsConfig")]
    public sealed class CharacterStatsConfig : ScriptableObject
    {
        public readonly CharacterCombatStats CombatStats = new();
        public readonly CharacterHealthStats HealthStats = new();
        public readonly CharacterStaminaStats StaminaStats = new();
        public readonly CharacterMoveStats MoveStats = new();
    }
}