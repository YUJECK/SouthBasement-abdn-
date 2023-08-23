using System;
using SouthBasement.Weapons;
using UnityEngine;

namespace SouthBasement.Characters.Stats
{
    [Serializable]
    public sealed class CharacterAttackStats
    {
        public int Damage => (int) (CurrentStats.Damage * DamageMultiplier);
        public CombatStats CurrentStats { get; set; } = new();
        public float DamageMultiplier { get; set; } = 1f;
        [field: SerializeField] public CombatStats DefaultCombatStats { get; private set; } = new();
    }
}