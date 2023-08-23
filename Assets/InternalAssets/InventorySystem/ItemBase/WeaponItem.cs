using SouthBasement.Items.Weapons;
using SouthBasement.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace SouthBasement.InventorySystem
{
    public abstract class WeaponItem : Item
    {
        [FormerlySerializedAs("CombatStats")] [SerializeField] private CombatStats combatStats;
        public CombatStats CombatStats => WeaponsStatsMultiplier.GetMultiplied(this);        
        
        public virtual void OnAttack(IDamagable[] damagables) { }

        public override string GetStatsDescription()
        {
            return $"Damage: {combatStats.Damage} \n" +
                   $"AttackRange: {combatStats.AttackRange} \n" +
                   $"AttackRate: {combatStats.AttackRate} \n" +
                   $"Stamina Require: {combatStats.StaminaRequire}";
        }

        public virtual void OnEquip() {}
        public virtual void OnUnequip() {}
    }
}