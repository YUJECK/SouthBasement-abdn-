using System;
using System.Collections.Generic;
using Zenject;

namespace TheRat.InventorySystem
{
    public sealed class Inventory
    {
        private DiContainer _container;

        public event Action<Item> OnAdded; 
        public event Action<Item> OnRemoved;

        private readonly Dictionary<string, Item> _items = new();
        public int InventorySize { get; private set; } = 4;

        public Inventory(DiContainer container)
        {
            _container = container;
        }

        public void AddItem(Item item)
        {
            if (_items.Count < InventorySize && _items.TryAdd(item.ItemID, item))
                OnAdded?.Invoke(item);
        }

        public void RemoveItem(Item item)
        {
            if (_items.ContainsKey(item.ItemID))
            {
                _items.Remove(item.ItemID);
                OnRemoved?.Invoke(_items[item.ItemID]);
            }
        }
    }
}