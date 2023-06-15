using System;
using TheRat.InventorySystem;

namespace TheRat
{
    public interface IAttackable : IDisposable
    {
        event Action<float> OnAttacked;
        
        WeaponItem Weapon { get; }
        
        void Attack();
    }
}