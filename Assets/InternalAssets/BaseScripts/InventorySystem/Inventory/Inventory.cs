using System;
using System.Collections.Generic;
using Zenject;

namespace TheRat.InventorySystem
{
    public sealed class Inventory
    {
        private DiContainer _container;
 
        public event Action<ActiveItem> OnAddedActiveItem; 
        public event Action<WeaponItem> OnAddedWeapon; 
        public event Action<Item> OnRemoved;

        private readonly Dictionary<string, Item> _junkItems = new();
        private readonly Dictionary<string, ActiveItem> _activeItems = new();
        private readonly Dictionary<string, WeaponItem> _weaponItems = new();
        
        public int ActiveItemsSlots { get; private set; } = 2;
        public int WeaponItemsSlots { get; private set; } = 3;

        public Inventory(DiContainer container)
        {
            _container = container;
        }

        public void AddItem(Item item)
        {
            if(item is ActiveItem activeItem && _activeItems.Count < ActiveItemsSlots)
                if(_activeItems.TryAdd(activeItem.ItemID, activeItem))
                    OnAddedActiveItem?.Invoke(activeItem);
            
            if(item is WeaponItem weaponItem && _weaponItems.Count < WeaponItemsSlots)
                if(_weaponItems.TryAdd(weaponItem.ItemID, weaponItem))
                    OnAddedWeapon?.Invoke(weaponItem);
        }

        public void RemoveItem(Item item)
        {
            if (_activeItems.ContainsKey(item.ItemID))
            {
                _activeItems.Remove(item.ItemID);
                OnRemoved?.Invoke(_activeItems[item.ItemID]);
            }
        }
    }
}