using System;
using System.Collections.Generic;
using SouthBasement.Extensions.DataStructures;

namespace SouthBasement.InventorySystem
{
    public sealed class TypeContainer : ITypeContainer
    {
        public Type ContainerType { get; private set; }
        public int ItemsCount => GetAllInContainer().Length;
        
        public event Action<Item> OnAdded;
        public event Action<string> OnRemoved;

        //item tag/item ID/item
        private Dictionary<string, Dictionary<string, Item>> _tagsContainers = new();
        private const string All = "all";

        public void Init<TContainerType>()
            where TContainerType : Item
        {
            ContainerType = typeof(TContainerType);
            _tagsContainers.Add(All, new Dictionary<string, Item>());
        }

        public void Init(Type type)
        {
            if (type.BaseType == typeof(Item))
                return;
            
            _tagsContainers.Add(All, new Dictionary<string, Item>());
            ContainerType = type;
        }

        public bool TryGetItem(string id, out Item item)
            => _tagsContainers[All].TryGetValue(id, out item);

        public Item[] GetAllInContainer()
        {
            return _tagsContainers[All].ToValueArray();
        }

        public bool RemoveItem(string id)
        {
            if (!_tagsContainers[All].ContainsKey(id))
                return false;

            Item item = _tagsContainers[All][id]; 
            _tagsContainers[All].Remove(id);

            foreach (var tag in item.ItemTags)
                _tagsContainers[tag].Remove(id);

            OnRemoved?.Invoke(id);
            return true;
        }

        public bool TryAddItem(Item item, Action<Item> callback = null)
        {
            if (item.GetItemType() != ContainerType)
                return false;

            if (!_tagsContainers[All].TryAdd(item.ItemID, item))
                return false;

            foreach (var tag in item.ItemTags)
            {
                if(!_tagsContainers.ContainsKey(tag))
                    _tagsContainers.Add(tag, new());
                
                _tagsContainers[tag].Add(item.ItemID, item);
            }

            OnAdded?.Invoke(item);
            return true;
        }
    }
}