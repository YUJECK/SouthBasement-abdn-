using System;
using SouthBasement.Characters;
using SouthBasement.InventorySystem;
using SouthBasement.Characters.Stats;
using SouthBasement.InternalAssets.InventorySystem.ItemBase;
using UnityEngine;
using Zenject;

namespace SouthBasement.Tests
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Food + "HealFood")]
    public sealed class HealFood : FoodItem
    {
        private CharacterStats _characterStats;
        private Inventory _inventory;
        [SerializeField] private int heal;

        [Inject]
        private void Construct(CharacterStats characterStats, Inventory inventory)
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
            _characterStats.HealthStats.SetHealth(_characterStats.HealthStats.CurrentHealth + heal);
            _inventory.RemoveItem(ItemID);
        }
    }
}