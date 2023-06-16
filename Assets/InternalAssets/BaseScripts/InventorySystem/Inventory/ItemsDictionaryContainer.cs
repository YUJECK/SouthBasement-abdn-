using System;
using System.Collections.Generic;
using TheRat.Extensions.DataStructures;

namespace TheRat.InventorySystem
{
    public sealed class ItemsDictionaryContainer
    {
        private readonly Dictionary<Type, Dictionary<string, Item>> _itemsContainers = new();

        public ItemsDictionaryContainer AddContainer<TItem>() where TItem : Item
        {
            _itemsContainers.TryAdd(typeof(TItem), new Dictionary<string, Item>());
            return this;
        } 

        public Item GetItem(string id)
        {
            foreach (var container in _itemsContainers)
            {
                if(container.Value.TryGetValue(id, out var item))
                    return item;
            }

            return null;
        }

        public TContainer[] GetAllInContainer<TContainer>() where TContainer : Item
        {
            if (_itemsContainers.TryGetValue(typeof(TContainer), out var container))
                return container.ToValueArray() as TContainer[];

            return null;
        }
        
        public bool RemoveItem(string id, Action<Type, object> callback)
        {
            foreach (var container in _itemsContainers)
            {
                if (container.Value.ContainsKey(id))
                {
                    callback?.Invoke(container.Value[id].GetType().BaseType, container.Value[id]);
                    container.Value.Remove(id);
                    
                    return true;
                }
            }

            return false;
        }

        public bool TryAddItem(Item item, Action<Type, object> callback)
        {
            var baseType = item.GetType().BaseType;

            if (_itemsContainers.ContainsKey(baseType))
            {
                if (_itemsContainers[baseType].TryAdd(item.ItemID, item))
                {
                    callback?.Invoke(baseType, item);
                    return true;
                }
                
            }

            return false;
        }
    }
}