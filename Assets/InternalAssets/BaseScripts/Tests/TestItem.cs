using System;
using SouthBasement.Characters;
using SouthBasement.InventorySystem;
using TheRat.Characters.Stats;
using UnityEngine;
using Zenject;

namespace SouthBasement.Tests
{
    [CreateAssetMenu]
    public sealed class TestItem : FoodItem
    {
        private CharacterHealthStats _characterStats;
        private Inventory _inventory;

        [Inject]
        private void Construct(CharacterHealthStats characterStats, Inventory inventory)
        {
            _inventory = inventory;
            _characterStats = characterStats;
        }
        
        public event Action OnUsed;

        public override Type GetItemType()
        {
            return typeof(ActiveItem);
        }

        public override void Eat()
        {
            _characterStats.SetHealth(_characterStats.CurrentHealth, _characterStats.MaximumHealth + 10);
            _inventory.RemoveItem(ItemID);
        }
    }
}