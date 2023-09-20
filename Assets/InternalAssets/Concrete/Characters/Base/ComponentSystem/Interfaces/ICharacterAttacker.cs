using System;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;

namespace SouthBasement.Characters.Components
{
    public interface ICharacterAttacker
    {
        public event Action<IDamagable[]> OnAttacked;

        public WeaponItem Weapon { get; }
        public bool Blocked { get; set; }
        
        public AttackResult Attack();
        public AttackResult DefaultAttack();
    }
}