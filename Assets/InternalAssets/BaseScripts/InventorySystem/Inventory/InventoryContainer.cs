using System;
using System.Collections.Generic;
using SouthBasement.Extensions.DataStructures;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    public sealed class InventoryContainer : IInventoryContainer
    {
        private readonly Dictionary<string, Item> _container = new();
        public Type ContainerType { get; private set; }
        
        public event Action<Type, Item> OnAdded; 
        public event Action<Type, Item> OnRemoved;

        public int ItemsCount => _container.Count;
        
        public void Init<TContainerType>() where TContainerType : Item
            => ContainerType = typeof(TContainerType);

        public Item GetItem(string id)
        {
            if (_container.TryGetValue(id, out var item))
                return item;

            Debug.LogError($"Cannot find item {id}");            
            return null;
        }
        
        public bool TryGetItem(string id, out Item item)
        {
            if (_container.TryGetValue(id, out var foundItem))
            {
                item = foundItem;
                return true;
            }

            item = null;
            return false;
        }

        public Item[] GetAllInContainer()
            => _container.ToValueArray();

        public bool RemoveItem(string id, Action<Type, Item> callback = null)
        {
            if (_container.ContainsKey(id))
            {
                callback?.Invoke(ContainerType, _container[id]);
                OnRemoved?.Invoke(ContainerType, _container[id]);
                
                _container.Remove(id);

                return true;
            }

            return false;
        }

        public bool TryAddItem(Item item, Action<Type, Item> callback = null)
        {
            if (item.GetItemType() != ContainerType)
                return false; 
            
            if (_container.TryAdd(item.ItemID, item))
            {
                callback?.Invoke(ContainerType, item);
                OnAdded?.Invoke(ContainerType, item);
                
                return true;
            }
            
            return false;
        }
    }
}