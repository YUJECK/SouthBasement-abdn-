using SouthBasement.Characters.Stats;
using SouthBasement.Weapons;
using UnityEngine;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Infrastructure + "CharacterStatsConfig")]
    public sealed class CharacterStatsConfig : ScriptableObject
    {
        public CharacterCombatStats CombatStats = new(new CombatStats());
        public CharacterHealthStats HealthStats = new();
        public CharacterStaminaStats StaminaStats = new();
        public CharacterMoveStats MoveStats = new();
    }
}