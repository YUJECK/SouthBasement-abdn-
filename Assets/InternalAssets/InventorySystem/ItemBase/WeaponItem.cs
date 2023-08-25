using SouthBasement.Items.Weapons;
using SouthBasement.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace SouthBasement.InventorySystem
{
    public abstract class WeaponItem : Item
    {
        [field: FormerlySerializedAs("CombatStats")] 
        [field: SerializeField] public CombatStats OriginalCombatStats
        {
            get;
            private set;
        }
        public CombatStats CombatStats => WeaponsStatsMultiplier.GetMultiplied(this);        
        
        public virtual void OnAttack(IDamagable[] damagables) { }

        public override string GetStatsDescription()
        {
            return $"Damage: {CombatStats.Damage} \n" +
                   $"AttackRange: {CombatStats.AttackRange} \n" +
                   $"AttackRate: {CombatStats.AttackRate} \n" +
                   $"Stamina Require: {CombatStats.StaminaRequire}";
        }

        public virtual void OnEquip() {}
        public virtual void OnUnequip() {}
    }
}