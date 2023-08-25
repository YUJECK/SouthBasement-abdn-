using System;
using SouthBasement.Characters.Stats;
using UnityEngine;

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
            inventory.OnRemoved += RemoveCheckCurrent;
        }

        private void RemoveCheckCurrent(string itemID)
        {
            if(CurrentWeapon == null)
                return;
                
            if(CurrentWeapon.ItemID == itemID)
            {
                CurrentWeapon.OnUnequip();
                CurrentWeapon = null;
                _attackStats.SetStats(_attackStats.DefaultCombatStats);
                OnSelectedNull?.Invoke();
            }
        }

        ~WeaponsUsage()
        {
            _inventory.OnAdded -= SetCurrent;
            _inventory.OnRemoved -= RemoveCheckCurrent;
        }

        public void SetCurrent(Item item)
        {
            if(CurrentWeapon == item)
                return;
            
            if (item.GetItemType() == typeof(WeaponItem))
            {
                if (CurrentWeapon != null) 
                    CurrentWeapon.OnUnequip();
                
                CurrentWeapon = item as WeaponItem;
                CurrentWeapon.OnEquip();
                _attackStats.SetStats(CurrentWeapon.CombatStats);
                Debug.Log(CurrentWeapon.CombatStats.AttackTags.Count);
                
                OnSelected?.Invoke(CurrentWeapon);
            }
        }
    }
}