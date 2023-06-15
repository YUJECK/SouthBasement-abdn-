using System;
using TheRat.InventorySystem;

namespace TheRat
{
    public interface IAttackable
    {
        event Action<float> OnAttacked;
        
        WeaponItem Weapon { get; }
        
        void Attack();
    }
}