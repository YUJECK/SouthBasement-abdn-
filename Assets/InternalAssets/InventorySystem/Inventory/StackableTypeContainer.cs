using System;
using System.Collections.Generic;
using SouthBasement.Extensions.DataStructures;
using SouthBasement.InternalAssets.InventorySystem.ItemBase;
using SouthBasement.Items;

namespace SouthBasement.InventorySystem
{
    public sealed class StackableTypeContainer : ITypeContainer
    {
        public Type ContainerType { get; private set; }
        public int ItemsCount => GetAllInContainer().Length;
        
        public event Action<Item> OnAdded;
        public event Action<string> OnRemoved;

        //item tag/item ID/item
        private Dictionary<ItemsTags, Dictionary<string, List<Item>>> _tagsContainers = new();
        private const ItemsTags All = ItemsTags.All;

        public void Init<TContainerType>()
            where TContainerType : Item
        {
            ContainerType = typeof(TContainerType);
            _tagsContainers.Add(All, new Dictionary<string, List<Item>>());
        }

        public void Init(Type type)
        {
            if (type.BaseType == typeof(Item))
                return;
            
            _tagsContainers.Add(All, new Dictionary<string, List<Item>>());
            ContainerType = type;
        }

        public bool TryGetItem(string id, out Item item)
        {
            if (_tagsContainers[All].TryGetValue(id, out var items))
            {
                item = items[0];
                return true;
            }

            item = null;
            return false;
        }

        public Item[] GetAllInContainer()
        {
            var lists = _tagsContainers[All].ToValueArray();
            var result = new List<Item>();
            
            foreach (var list in lists)
                result.AddRange(list);

            return result.ToArray();
        }

        public bool RemoveItem(string id)
        {
            if (!_tagsContainers[All].ContainsKey(id))
                return false;

            Item item = _tagsContainers[All][id][0]; 
            _tagsContainers[All][id].RemoveAt(0);

            if (_tagsContainers[All][id].Count == 0)
                _tagsContainers[All].Remove(id);
                
            foreach (var tag in item.ItemTags)
            {
                _tagsContainers[tag][id].RemoveAt(0);

                if (_tagsContainers[tag][id].Count == 0)
                    _tagsContainers[tag].Remove(id);
            }
            
            OnRemoved?.Invoke(id);
            return true;
        }

        public bool TryAddItem(Item item, Action<Item> callback = null)
        {
            if (item.GetItemType() != ContainerType)
                return false;

            if (_tagsContainers[All].ContainsKey(item.ItemID))
                _tagsContainers[All][item.ItemID].Add(item);
            else
            {
                _tagsContainers[All].Add(item.ItemID, new List<Item>() { item });
            }


            foreach (var tag in item.ItemTags)
            {
                if (!_tagsContainers.ContainsKey(tag))
                    _tagsContainers.Add(tag, new());
                
                if(!_tagsContainers[tag].ContainsKey(item.ItemID))
                    _tagsContainers[tag].Add(item.ItemID, new());
                
                _tagsContainers[tag][item.ItemID].Add(item);
            }
            OnAdded?.Invoke(item);
            return true;
        }
    }
}