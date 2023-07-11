﻿using SouthBasement.Weapons;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    public abstract class WeaponItem : Item
    {
        [field: SerializeField] public AttackStatsConfig AttackStatsConfig { get; private set; }
        
        public virtual void OnAttack(IDamagable[] damagables) { }

        public override string GetStatsDescription()
        {
            return $"Damage: {AttackStatsConfig.Damage} \n" +
                   $"AttackRange: {AttackStatsConfig.AttackRange} \n" +
                   $"AttackRate: {AttackStatsConfig.AttackRate} \n" +
                   $"Stamina Require: {AttackStatsConfig.StaminaRequire}";
        }

        public virtual void OnEquip() {}
        public virtual void OnRemoved() {}
    }
}