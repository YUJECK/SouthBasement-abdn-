using System;
using UnityEngine;
using Zenject;

namespace TheRat.InventorySystem.Weapons
{
    public class WeaponItemsInventoryHUD : MonoBehaviour
    {
        private WeaponSlot[] _slots;
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory) 
            => _inventory = inventory;

        private void Awake()
            => _slots = GetComponentsInChildren<WeaponSlot>();

        private void OnEnable()
        {
            _inventory.OnAddedWeapon.OnAdded += OnAddedWeapon;
            _inventory.OnAddedWeapon.OnRemoved += OnRemoved;
        }

        private void OnDisable()
        {
            _inventory.OnAddedWeapon.OnAdded -= OnAddedWeapon;
            _inventory.OnAddedWeapon.OnRemoved -= OnRemoved;
        }

        private void OnAddedWeapon(WeaponItem item)
        {
            GetEmpty()?.SetItem(item);
        }

        private void OnRemoved(Item item)
        {
            Find(item)?.SetItem(null);
        }
        
        private WeaponSlot Find(Item item)
        {
            foreach (var slot in _slots)
            {
                if (slot.CurrentItem.ItemID == item.ItemID)
                    return slot;
            }

            return null;
        }

        private WeaponSlot GetEmpty()
        {
            foreach (var slot in _slots)
            {
                if (slot.CurrentItem == null)
                    return slot;
            }
            
            return null;
        }
    }
}