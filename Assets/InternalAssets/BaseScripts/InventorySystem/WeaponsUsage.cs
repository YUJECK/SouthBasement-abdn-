using System;
using SouthBasement.InputServices;

namespace SouthBasement.InventorySystem
{
    public sealed class WeaponsUsage
    {
        public WeaponItem CurrentWeapon { get; private set; }
        private ActiveItemUsage _activeItemUsage;
        private readonly CharacterStats _characterStats;
        private readonly Inventory _inventory;

        public event Action<WeaponItem> OnSelected;
        
        public WeaponsUsage(Inventory inventory, CharacterStats characterStats)
        {
            _characterStats = characterStats;
            _inventory = inventory;
            
            inventory.OnAdded += SetCurrent;
        }

        ~WeaponsUsage()
        {
            _inventory.OnAdded -= SetCurrent;
        }

        public void SetCurrent(Item item)
        {
            if (item.GetItemType() == typeof(WeaponItem))
            {
                CurrentWeapon = item as WeaponItem;
                _characterStats.WeaponStats = CurrentWeapon.WeaponStats;
                
                OnSelected?.Invoke(CurrentWeapon);
            }
        }
    }
}