using SouthBasement.Characters;
using SouthBasement.Economy;
using SouthBasement.InventorySystem.ItemBase;
using UnityEngine;
using Zenject;

namespace SouthBasement.BaseScripts.Tests.StingyHeart
{
    [CreateAssetMenu(menuName = AssetMenuHelper.PassiveItem + "StingyHeart")]
    public sealed class StingyHeart : PassiveItem
    {
        private CheeseService _cheeseService;
        private CharacterStats _characterStats;

        [Inject]
        private void Construct(CheeseService cheeseService, CharacterStats characterStats)
        {
            _cheeseService = cheeseService;
            _characterStats = characterStats;
        }

        public override void OnAddedToInventory()
        {
            _cheeseService.CheeseCoefficient += 0.4f;
            
            _characterStats.HealthStats.SetHealth(_characterStats.HealthStats.CurrentHealth - 15,
                _characterStats.HealthStats.CurrentHealth - 15);
        }

        public override void OnRemovedFromInventory()
        {
            _cheeseService.CheeseCoefficient -= 0.4f;
            
            _characterStats.HealthStats.SetHealth(_characterStats.HealthStats.CurrentHealth + 15,
                _characterStats.HealthStats.CurrentHealth + 15);
        }
    }
}