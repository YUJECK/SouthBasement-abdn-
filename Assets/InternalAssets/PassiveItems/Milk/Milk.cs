using System;
using SouthBasement.Characters.Stats;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace TheRat
{
    [CreateAssetMenu]
    public class Milk : PassiveItem
    {
        private CharacterHealthStats _healthStats;
        
        public override Type GetItemType() => typeof(PassiveItem);

        [Inject]
        private void Construct(CharacterHealthStats characterHealthStats)
        {
            _healthStats = characterHealthStats;
        }
        
        public override void OnPutOn()
        {
            _healthStats.SetHealth(_healthStats.CurrentHealth, _healthStats.MaximumHealth + 10);    
        }
        public override void OnPutOut()
        {
            _healthStats.SetHealth(_healthStats.CurrentHealth, _healthStats.MaximumHealth - 10);    
        }
    }
}
