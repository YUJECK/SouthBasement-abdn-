using System;
using SouthBasement.Characters;
using SouthBasement.InventorySystem;
using SouthBasement.Characters.Stats;
using SouthBasement.InventorySystem.ItemBase;
using UnityEngine;
using Zenject;

namespace SouthBasement.Tests
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Food + "HealFood")]
    public sealed class HealFood : FoodItem
    {
        [SerializeField] private int heal;

        private CharacterStats _characterStats;
        private Inventory _inventory;

        public event Action OnUsed;

        [Inject]
        private void Construct(CharacterStats characterStats, Inventory inventory)
        {
            _inventory = inventory;
            _characterStats = characterStats;
        }

        public override string GetStatsDescription()
        {
            return $"Heals { heal } hp";
        }

        public override void Eat()
        {
            _characterStats.HealthStats.SetHealth(_characterStats.HealthStats.CurrentHealth + heal);
            _inventory.RemoveItem(ItemID);
        }
    }
}