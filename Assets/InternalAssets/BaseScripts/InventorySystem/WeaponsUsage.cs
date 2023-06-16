using System;
using TheRat.InputServices;

namespace TheRat.InventorySystem
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
            
            inventory.OnAddedWeapon.OnAdded += SetCurrent;
        }

        ~WeaponsUsage()
        {
            _inventory.OnAddedWeapon.OnAdded += SetCurrent;
        }

        public void SetCurrent(WeaponItem item)
        {
            CurrentWeapon = item;
            _characterStats.WeaponStats = item.WeaponStats;
            
            OnSelected?.Invoke(item);
        }
    }
}