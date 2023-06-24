using System;
using SouthBasement.InventorySystem;

namespace SouthBasement.Characters.Components
{
    public abstract class CharacterAttackable<TOwner> : CharacterComponent<TOwner>, IAttackable
    {
        public event Action<float> OnAttacked;

        public WeaponItem Weapon { get; }
        public bool Blocked { get; set; }
        
        public abstract void Attack();
    }
}