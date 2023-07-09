using System;
using SouthBasement.InventorySystem;
using SouthBasement.Characters.Stats;
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

        public override string GetStatsDescription()
        {
            return "Heals 10 hp";
        }

        public override Type GetItemType()
        {
            return typeof(FoodItem);
        }

        public override void Eat()
        {
            _characterStats.SetHealth(_characterStats.CurrentHealth + 10);
            _inventory.RemoveItem(ItemID);
        }
    }
}