using System;
using TheRat.InputServices;

namespace TheRat.InventorySystem
{
    public class WeaponsUsage
    {
        public WeaponItem CurrentWeapon { get; private set; }
        private ActiveItemUsage _activeItemUsage;
        private readonly CharacterStats _characterStats;

        public event Action<WeaponItem> OnSelected;
        
        public WeaponsUsage(IInputService service, Inventory inventory, CharacterStats characterStats)
        {
            _characterStats = characterStats;
            inventory.OnAddedWeapon += item => CurrentWeapon = item;
        }

        public void SetCurrent(WeaponItem item)
        {
            CurrentWeapon = item;
            _characterStats.WeaponStats = item.WeaponStats;
            
            OnSelected?.Invoke(item);
        }
    }
}