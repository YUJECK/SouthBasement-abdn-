using System;
using SouthBasement.InventorySystem;
using SouthBasement.Characters.Stats;
using UnityEngine;
using Zenject;

namespace SouthBasement.Tests
{
    [CreateAssetMenu]
    public sealed class HealFood : FoodItem
    {
        private CharacterHealthStats _characterStats;
        private Inventory _inventory;
        [SerializeField] private int heal;

        [Inject]
        private void Construct(CharacterHealthStats characterStats, Inventory inventory)
        {
            _inventory = inventory;
            _characterStats = characterStats;
        }
        
        public event Action OnUsed;

        public override string GetStatsDescription()
        {
            return $"Heals { heal } hp";
        }

        public override Type GetItemType()
        {
            return typeof(FoodItem);
        }

        public override void Eat()
        {
            _characterStats.SetHealth(_characterStats.CurrentHealth + heal);
            _inventory.RemoveItem(ItemID);
        }
    }
}