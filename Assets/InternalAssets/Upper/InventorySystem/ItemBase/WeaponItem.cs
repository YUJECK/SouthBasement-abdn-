﻿using System;
using SouthBasement.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace SouthBasement.InventorySystem.ItemBase
{
    public abstract class WeaponItem : Item
    {
        [field: FormerlySerializedAs("CombatStats")] 
        [field: SerializeField] public CombatStats OriginalCombatStats
        {
            get;
            private set;
        }

        public CombatStats CombatStats => OriginalCombatStats;
        
        public override Type GetItemType()
            => typeof(WeaponItem);
        
        public virtual void OnAttack(AttackResult result) { }

        public override string GetStatsDescription()
        {
            return $"Damage: {CombatStats.Multiplied.Damage} \n" +
                   $"AttackRange: {CombatStats.Multiplied.AttackRange} \n" +
                   $"AttackRate: {CombatStats.Multiplied.AttackRate} \n" +
                   $"Stamina Require: {CombatStats.Multiplied.StaminaRequire}";
        }

        public virtual void OnEquip() {}
        public virtual void OnTakeOff() {}
    }
}