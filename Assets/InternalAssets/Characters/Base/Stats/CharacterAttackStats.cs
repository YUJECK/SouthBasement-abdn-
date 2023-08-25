using System;
using SouthBasement.Items.Weapons;
using SouthBasement.Weapons;
using UnityEngine;
using UnityEngine.Rendering;

namespace SouthBasement.Characters.Stats
{
    [Serializable]
    public sealed class CharacterAttackStats
    {
        [field: SerializeField] public CombatStats DefaultCombatStats { get; private set; } = new();
        
        public CombatStats CurrentStats => _currentStats;

        private CombatStats _currentStats;

        public CharacterAttackStats()
            => SetStats(DefaultCombatStats);

        public void SetStats(CombatStats combatStats)
        {
            if (combatStats != null)
                _currentStats = combatStats;
        }
    }
}