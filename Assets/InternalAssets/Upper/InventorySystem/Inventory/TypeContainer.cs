using System;
using System.Collections.Generic;
using SouthBasement.Extensions.DataStructures;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.Items;

namespace SouthBasement.InventorySystem
{
    public sealed class TypeContainer : ITypeContainer
    {
        public Type ContainerType { get; private set; }
        public int ItemsCount => GetAllInContainer().Length;
        
        public event Action<Item> OnAdded;
        public event Action<string> OnRemoved;

        //item tag/item ID/item
        private Dictionary<ItemsTags, Dictionary<string, Item>> _tagsContainers = new();

        public void Init<TContainerType>()
            where TContainerType : Item
        {
            ContainerType = typeof(TContainerType);
            _tagsContainers.Add(ItemsTags.All, new Dictionary<string, Item>());
        }

        public void Init(Type type)
        {
            if (type.BaseType == typeof(Item))
                return;
            
            _tagsContainers.Add(ItemsTags.All, new Dictionary<string, Item>());
            ContainerType = type;
        }

        public bool TryGetItem(string id, out Item item)
            => _tagsContainers[ItemsTags.All].TryGetValue(id, out item);

        public Item[] GetAllInContainer()
        {
            return _tagsContainers[ItemsTags.All].ToValueArray();
        }

        public bool RemoveItem(string id)
        {
            if (!_tagsContainers[ItemsTags.All].ContainsKey(id))
                return false;

            Item item = _tagsContainers[ItemsTags.All][id]; 
            _tagsContainers[ItemsTags.All].Remove(id);

            foreach (var tag in item.ItemTags)
                _tagsContainers[tag].Remove(id);

            OnRemoved?.Invoke(id);
            return true;
        }

        public bool TryAddItem(Item item, Action<Item> callback = null)
        {
            if (item.GetItemType() != ContainerType)
                return false;

            if (!_tagsContainers[ItemsTags.All].TryAdd(item.ItemID, item))
                return false;

            foreach (var tag in item.ItemTags)
            {
                if(!_tagsContainers.ContainsKey(tag))
                    _tagsContainers.Add(tag, new Dictionary<string, Item>());
                
                _tagsContainers[tag].Add(item.ItemID, item);
            }

            OnAdded?.Invoke(item);
            return true;
        }
    }
}