using System;
using SouthBasement.InventorySystem;

namespace SouthBasement.Characters.Components
{
    public interface IAttacker
    {
        public event Action<IDamagable[]> OnAttacked;

        public WeaponItem Weapon { get; }
        public bool Blocked { get; set; }
        
        public void Attack();
    }
}