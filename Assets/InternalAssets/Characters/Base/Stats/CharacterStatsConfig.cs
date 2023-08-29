using SouthBasement.Characters.Stats;
using UnityEngine;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Infrastructure+"CharacterStatsConfig")]
    public sealed class CharacterStatsConfig : ScriptableObject
    {
        public CharacterCombatStats CombatStats = new();
        public CharacterHealthStats HealthStats = new();
        public CharacterStaminaStats StaminaStats = new();
        public CharacterMoveStats MoveStats = new();
    }
}