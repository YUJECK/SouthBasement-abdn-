using System;
using SouthBasement.Characters;
using SouthBasement.Characters.Stats;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.PassiveItem + "Milk")]
    public class Milk : PassiveItem
    {
        private CharacterStats _healthStats;

        [Inject]
        private void Construct(CharacterStats characterStats)
            => _healthStats = characterStats;

        public override void OnAddedToInventory()
        {
            _healthStats.HealthStats.SetHealth(_healthStats.HealthStats.CurrentHealth, _healthStats.HealthStats.MaximumHealth + 10);    
        }
        public override void OnRemovedFromInventory()
        {
            _healthStats.HealthStats.SetHealth(_healthStats.HealthStats.CurrentHealth, _healthStats.HealthStats.MaximumHealth - 10);    
        }
    }
}
