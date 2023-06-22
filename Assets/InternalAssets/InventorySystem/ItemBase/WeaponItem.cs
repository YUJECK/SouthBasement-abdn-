using SouthBasement.Weapons;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    public abstract class WeaponItem : Item
    {
        [field: SerializeField] public AttackStatsConfig AttackStatsConfig { get; private set; }
        
        public abstract void OnAttack();
    }
}