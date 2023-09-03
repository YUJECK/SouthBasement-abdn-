using System;
using System.Collections.Generic;
using SouthBasement.InternalAssets.InventorySystem.ItemBase;

namespace SouthBasement.InventorySystem
{
    public sealed class StackableInventoryContainer : IInventoryContainer
    {
        public Type ContainerType { get; private set; }
        public int ItemsCount => _container.Count;

        public event Action<Type, Item> OnAdded;
        public event Action<Type, Item> OnRemoved;

        private Dictionary<string, List<Item>> _container = new();

        public void Init<TContainerType>() where TContainerType : Item 
            => ContainerType = typeof(TContainerType);

        public void Init(Type type)
        {
            ContainerType = type;
        }

        public Item GetItem(string id)
        {
            if(_container.TryGetValue(id, out var value))
                if (value.Count > 0)
                    return value[0];

            return null;
        }

        public bool TryGetItem(string id, out Item item)
        {
            if(_container.TryGetValue(id, out var value))
                if (value.Count > 0)
                {
                    item = value[0];
                    return true;
                }

            item = null;
            return false;
        }

        public Item[] GetAllInContainer()
        {
            List<Item> items = new();

            foreach (var itemsPair in _container)
                items.AddRange(itemsPair.Value);

            return items.ToArray();
        }

        public bool RemoveItem(string id, Action<Type, Item> callback = null)
        {
            if (_container.TryGetValue(id, out var itemsList) && itemsList.Count > 0)
            {
                callback?.Invoke(ContainerType, itemsList[0]);
                itemsList.RemoveAt(0);
                
                return true;
            }

            return false;
        }

        public bool TryAddItem(Item item, Action<Type, Item> callback = null)
        {
            if (_container.TryGetValue(item.ItemID, out var value))
            {
                value.Add(item);
                return true;
            }

            return _container.TryAdd(item.ItemID, new List<Item> {item});
        }
    }
}