using System;
using TheRat.InventorySystem;
using TheRat.Characters;
using UnityEngine;
using Zenject;

namespace TheRat.Tests
{
    [CreateAssetMenu]
    public sealed class TestItem : ActiveItem
    {
        private CharacterStats _characterStats;
        private Inventory _inventory;

        [Inject]
        private void Construct(CharacterStats characterStats, Inventory inventory)
        {
            _inventory = inventory;
            _characterStats = characterStats;
        }
        
        public event Action OnUsed;
        
        public override void Use()
        {
            _characterStats.SetHealth(_characterStats.CurrentHealth, _characterStats.MaximumHealth + 10);
            _inventory.RemoveItem(this.ItemID);

            OnUsed?.Invoke();
        }
    }
}