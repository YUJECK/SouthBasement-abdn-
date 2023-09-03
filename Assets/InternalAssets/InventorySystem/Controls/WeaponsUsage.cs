using System;
using SouthBasement.Characters.Stats;
using SouthBasement.InternalAssets.InventorySystem.ItemBase;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    public sealed class WeaponsUsage
    {
        public WeaponItem CurrentWeapon { get; private set; }
        private ActiveItemUsage _activeItemUsage;
        
        private readonly CharacterCombatStats _combatStats;
        private readonly Inventory _inventory;

        public event Action<WeaponItem> OnSelected;
        public event Action OnSelectedNull;
        
        public WeaponsUsage(Inventory inventory, CharacterCombatStats combatStats)
        {
            _combatStats = combatStats;
            _inventory = inventory;
            
            inventory.OnAdded += SetCurrent;
            inventory.OnRemoved += RemoveCheckCurrent;
        }
        ~WeaponsUsage()
        {
            _inventory.OnAdded -= SetCurrent;
            _inventory.OnRemoved -= RemoveCheckCurrent;
        }

        private void RemoveCheckCurrent(string itemID)
        {
            if(CurrentWeapon == null)
                return;
                
            if(CurrentWeapon.ItemID == itemID)
            {
                CurrentWeapon.OnTakeOff();
                CurrentWeapon = null;
                _combatStats.SetStats(_combatStats.DefaultStats);
                OnSelectedNull?.Invoke();
            }
        }
        
        public void SetCurrent(Item item)
        {
            if(CurrentWeapon == item)
                return;
            
            if (item.GetItemType() == typeof(WeaponItem))
            {
                if (CurrentWeapon != null) 
                    CurrentWeapon.OnTakeOff();
                
                CurrentWeapon = item as WeaponItem;
                CurrentWeapon.OnEquip();
                _combatStats.SetStats(CurrentWeapon.CombatStats);
                
                OnSelected?.Invoke(CurrentWeapon);
            }
        }
    }
}