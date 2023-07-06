using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SouthBasement.InventorySystem
{
    public sealed class PassiveItemsUsage : IInitializable, ITickable, IDisposable 
    {
        private Dictionary<string, PassiveItem> _passiveItems = new();
        private Inventory _inventory;

        public PassiveItemsUsage(Inventory inventory) 
            => _inventory = inventory;

        private void OnRemoved(string itemID)
        {
            if (!_passiveItems.ContainsKey(itemID)) return;
            
            _passiveItems[itemID].OnPutOut();
            _passiveItems.Remove(itemID);
        }

        private void OnAddedItem(Item item)
        {
            Debug.Log("skdjflksdjfk");
            
            if (item is PassiveItem passiveItem)
            {
                Debug.Log("skdjflk76sdjfk");
                
                _passiveItems.Add(passiveItem.ItemID, passiveItem);
                passiveItem.OnPutOn();
            }
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