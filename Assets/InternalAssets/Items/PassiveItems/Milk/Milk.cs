using System;
using SouthBasement.Characters;
using SouthBasement.Characters.Stats;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.PassiveItem + "Milk")]
    public class Milk : PassiveItem
    {
        private CharacterStats _healthStats;
        
        public override Type GetItemType() => typeof(PassiveItem);

        [Inject]
        private void Construct(CharacterStats characterStats)
            => _healthStats = characterStats;

        public override void OnPutOn()
        {
            _healthStats.HealthStats.SetHealth(_healthStats.HealthStats.CurrentHealth, _healthStats.HealthStats.MaximumHealth + 10);    
        }
        public override void OnPutOut()
        {
            _healthStats.HealthStats.SetHealth(_healthStats.HealthStats.CurrentHealth, _healthStats.HealthStats.MaximumHealth - 10);    
        }
    }
}
