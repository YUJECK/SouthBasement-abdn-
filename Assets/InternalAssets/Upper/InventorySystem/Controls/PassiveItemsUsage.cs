using System;
using System.Collections.Generic;
using SouthBasement.InventorySystem.ItemBase;
using UnityEngine;
using Zenject;

namespace SouthBasement.InventorySystem
{
    public sealed class PassiveItemsUsage : IInitializable, ITickable, IDisposable 
    {
        private readonly Dictionary<string, PassiveItem> _passiveItems = new();
        private readonly Inventory _inventory;

        public PassiveItemsUsage(Inventory inventory) 
            => _inventory = inventory;

        private void OnRemoved(string itemID)
        {
            if (!_passiveItems.ContainsKey(itemID)) return;
            
            _passiveItems.Remove(itemID);
        }

        private void OnAddedItem(Item item)
        {
            if (item is PassiveItem passiveItem)
                _passiveItems.Add(passiveItem.ItemID, passiveItem);
        }
        
        public void Initialize()
        {
            _inventory.OnAdded += OnAddedItem;
            _inventory.OnRemoved += OnRemoved;
        }
        public void Dispose()
        {
            _inventory.OnAdded -= OnAddedItem;
            _inventory.OnRemoved -= OnRemoved;
        }

        public void Tick()
        {            
            foreach(var passiveItem in _passiveItems)
                passiveItem.Value.OnRun();
        }
    }
}