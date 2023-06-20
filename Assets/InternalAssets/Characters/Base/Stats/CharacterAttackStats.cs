﻿using System;
using SouthBasement.Weapons;
using UnityEngine;

namespace SouthBasement.Characters.Stats
{
    [Serializable]
    public sealed class CharacterAttackStats
    {
        public AttackStatsConfig CurrentStats { get; set; } = new();
        [field: SerializeField] public AttackStatsConfig DefaultAttackStatsConfig { get; private set; } = new();
    }
}