using System;
using SouthBasement.Weapons;
using UnityEngine;

namespace SouthBasement.Characters.Stats
{
    [Serializable]
    public sealed class CharacterAttackStats
    {
        public AttackStatsConfig CurrentStats { get; set; } = new();
        public float DamageMultiplier { get; set; } 
        [field: SerializeField] public AttackStatsConfig DefaultAttackStatsConfig { get; private set; } = new();
    }
}