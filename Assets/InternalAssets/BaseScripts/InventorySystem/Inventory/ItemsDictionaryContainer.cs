using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    public sealed class ItemsDictionaryContainer
    {
        private readonly Dictionary<Type, Dictionary<string, IInventoryContainer>> _itemsContainers = new();

        public ItemsDictionaryContainer AddContainer<TContainer>(IInventoryContainer container, string mainSubContainer = "any")
            where TContainer : Item
        {
            if (_itemsContainers.TryAdd(typeof(TContainer), new Dictionary<string, IInventoryContainer>()))
            {
                var newContainer = container;
                newContainer.Init<TContainer>();
                
                _itemsContainers[typeof(TContainer)].TryAdd(mainSubContainer, newContainer);
            }
            
            return this;
        }

        public ItemsDictionaryContainer AddSubContainerTo<TContainer>(string subcontaienr) where TContainer : Item
        {
            if (_itemsContainers.TryGetValue(typeof(TContainer), out var container))
            {
                var newContainer = new InventoryContainer();
                newContainer.Init<TContainer>();
                
                container.TryAdd(subcontaienr, newContainer);
            }
            
            return this;
        }
        public bool TryAddItem(Item item, string subcontainerID = "any") 
        {
            if(item == null)
                Debug.LogError($"You tried to add null item to {item.GetItemType()} in subcontainer {subcontainerID}");
            
            var subcontainer = GetSubContainer(item.GetItemType(), subcontainerID);

            if (subcontainer == null)
            {
                Debug.LogError($"Cant add {item}");
                return false;
            }
            
            return subcontainer.TryAddItem(item);
        }
        public bool Remove(string itemID) 
        {
            var subcontainer = FindWithItem(itemID);

            if (subcontainer == null)
            {
                Debug.LogError($"{itemID} wasnt found");
                return false;
            }
            
            if(subcontainer.RemoveItem(itemID))
                return true;

            return false;
        }

        public Item[] GetAllInContainer<TContainer>() where TContainer : Item
        {
            var items = new List<Item>();

            _itemsContainers.TryGetValue(typeof(TContainer), out var container);
            {
                foreach (var subcontainer in container)
                {
                    if(subcontainer.Value.ItemsCount > 0)
                        items.AddRange(subcontainer.Value.GetAllInContainer());
                }
            }

            return items.ToArray();
        }

        public Item[] GetAllInSubContainerOfContainer<TContainer>(string subContainerID) 
            => GetSubContainer(typeof(TContainer), subContainerID).GetAllInContainer();

        private IInventoryContainer GetSubContainer(Type type, string subcontainerID)
        {
            if (_itemsContainers.TryGetValue(type, out var container))
            {
                container.TryGetValue(subcontainerID, out var subcontainer);
                return subcontainer;
            }
            
            Debug.LogError($"Cannot find subcontainer {subcontainerID} in {type}");
            return null;
        }

        private IInventoryContainer FindWithItem(string id)
        {
            foreach (var container in _itemsContainers)
            {
                foreach (var subcontainer in container.Value)
                {
                    if (subcontainer.Value.TryGetItem(id, out var item))
                        return subcontainer.Value;
                }
            }

            return null;
        }
    }
}