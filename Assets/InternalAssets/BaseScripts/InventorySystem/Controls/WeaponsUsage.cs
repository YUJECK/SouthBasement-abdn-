using System;
using TheRat.Characters.Stats;

namespace SouthBasement.InventorySystem
{
    public sealed class WeaponsUsage
    {
        public WeaponItem CurrentWeapon { get; private set; }
        private ActiveItemUsage _activeItemUsage;
        private readonly CharacterAttackStats _characterStats;
        private readonly Inventory _inventory;

        public event Action<WeaponItem> OnSelected;
        public event Action OnSelectedNull;
        
        public WeaponsUsage(Inventory inventory, CharacterAttackStats characterStats)
        {
            _characterStats = characterStats;
            _inventory = inventory;
            
            inventory.OnAdded += SetCurrent;
            inventory.OnRemoved += CheckCurrent;
        }

        private void CheckCurrent(string itemID)
        {
            if(CurrentWeapon == null)
                return;
                
            if(CurrentWeapon.ItemID == itemID)
            {
                CurrentWeapon = null;
                OnSelectedNull?.Invoke();
            }
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
                _characterStats.CurrentStats = CurrentWeapon.AttackStatsConfig;
                
                OnSelected?.Invoke(CurrentWeapon);
            }
        }
    }
}