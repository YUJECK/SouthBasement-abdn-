using System;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement.Tests
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
            var items = _inventory.MainContainer.GetAllInSubContainerOfContainer<WeaponItem>("bone_made");
            
            foreach (var item in items)
            {
                Debug.Log(item.ItemID);
            }
        }

        public override Type GetItemType()
        {
            return typeof(ActiveItem);
        }
    }
}