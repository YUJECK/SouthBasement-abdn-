using System;
using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    public sealed class ItemsDictionaryContainer
    {
        private readonly Dictionary<Type, ITypeContainer> _itemsContainers = new();
        private readonly Dictionary<Type, int> _containersLimits = new();
        
        public ItemsDictionaryContainer AddContainer<TContainer>(ITypeContainer container, int limit)
            where TContainer : Item
        {
            if (_itemsContainers.TryAdd(typeof(TContainer), container))
            {
                var newContainer = _itemsContainers[typeof(TContainer)];
                newContainer.Init<TContainer>();
                
                _containersLimits.TryAdd(typeof(TContainer), limit);
            }
            
            return this;
        }

        public void Clear()
        {
            _itemsContainers.Clear();
            _containersLimits.Clear();
        }
        
        public bool TryAddItem(Item item) 
        {
            if(item == null)
                Debug.LogError($"You tried to add null item");

            return _itemsContainers[item.GetItemType()].TryAddItem(item);
        }
        public bool Remove(string itemID) 
        {
            var subcontainer = FindWithItem(itemID);

            if (subcontainer == null)
            {
                Debug.LogError($"{itemID} wasnt found");
                return false;
            }
            
            return subcontainer.RemoveItem(itemID);
        }

        public Item[] GetAllInContainer<TContainer>() where TContainer : Item
        {
            if(_itemsContainers.TryGetValue(typeof(TContainer), out var container))
                return container.GetAllInContainer();

            return null;
        }

        public Item[] GetAllInContainerOfItemType(Item item)
        {
            if (_itemsContainers.TryGetValue(item.GetItemType(), out var container))
                return container.GetAllInContainer();

            return new List<Item>().ToArray();
        }

        public ITypeContainer FindWithItem(string id)
        {
            foreach (var typeContainerPair in _itemsContainers)
            {
                if (typeContainerPair.Value.TryGetItem(id, out var item))
                    return typeContainerPair.Value;
            }

            return null;
        }
    }
}