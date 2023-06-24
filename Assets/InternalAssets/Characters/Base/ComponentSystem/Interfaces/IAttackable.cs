using System;
using SouthBasement.InventorySystem;

namespace SouthBasement.Characters.Components
{
    public interface IAttackable
    {
        public event Action<float> OnAttacked;

        public WeaponItem Weapon { get; }
        public bool Blocked { get; set; }
        
        public void Attack();
    }
}