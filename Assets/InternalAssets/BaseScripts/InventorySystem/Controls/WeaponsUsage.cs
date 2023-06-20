﻿using System;
using SouthBasement.Characters.Stats;

namespace SouthBasement.InventorySystem
{
    public sealed class WeaponsUsage
    {
        public WeaponItem CurrentWeapon { get; private set; }
        private ActiveItemUsage _activeItemUsage;
        private readonly CharacterAttackStats _attackStats;
        private readonly Inventory _inventory;

        public event Action<WeaponItem> OnSelected;
        public event Action OnSelectedNull;
        
        public WeaponsUsage(Inventory inventory, CharacterAttackStats attackStats)
        {
            _attackStats = attackStats;
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
                _attackStats.CurrentStats = _attackStats.DefaultAttackStatsConfig;
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
                _attackStats.CurrentStats = CurrentWeapon.AttackStatsConfig;
                
                OnSelected?.Invoke(CurrentWeapon);
            }
        }
    }
}