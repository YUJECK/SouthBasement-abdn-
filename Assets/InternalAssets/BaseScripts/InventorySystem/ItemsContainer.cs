using System;
using System.Collections.Generic;
using SouthBasement.InventorySystem;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace SouthBasement.Items
{
    public sealed class ItemsContainer
    {
        private readonly Dictionary<Rarity, Dictionary<Type, Dictionary<string, Item>>> _items;
        private ItemPicker _itemPickerPrefab;

        private DiContainer _diContainer;
        
        public ItemsContainer(Item[] items, DiContainer diContainer)
        {
            _items = new Dictionary<Rarity, Dictionary<Type, Dictionary<string, Item>>>()
            {
                {Rarity.D, new()},
                {Rarity.C, new()},
                {Rarity.B, new()},
                {Rarity.A, new()},
            };
            
            Add(items);    
        }

        public ItemPicker SpawnItem(string id, Vector3 position)
        {
            var itemSO = ScriptableObject.Instantiate(Get(id));
            var item = _diContainer.InstantiatePrefabForComponent<ItemPicker>(_itemPickerPrefab, position, quaternion.identity, null);
            item.SetItem(itemSO);
            
            return item;
        }
        public ItemPicker SpawnItem(Item item, Vector3 position)
        {
            if (item == null)
            {
                Debug.Log("You tried to spawn null item");
                return null;
            }
                
            var picker = _diContainer.InstantiatePrefabForComponent<ItemPicker>(_itemPickerPrefab, position, quaternion.identity, null);
            picker.SetItem(item);
            
            return picker;
        }
        
        public void Add(Item[] toAdd)
        {
            foreach (var item in toAdd)
            {
                _items[item.Rarity].TryAdd(item.GetItemType(), new Dictionary<string, Item>());
                _items[item.Rarity][item.GetItemType()].TryAdd(item.ItemID, item);
            }
        }
        
        public Item Get(string id)
        {
            foreach (var itemsRarityContainer in _items)
            {
                foreach (var itemsTypeContainer in itemsRarityContainer.Value)
                {
                    if (itemsTypeContainer.Value.TryGetValue(id, out var value))
                        return value;
                }
            }

            return null;
        }

        // public Item GetRandomInRarity(Rarity rarity)
        // {
        //     var items = _items[rarity];
        //     return items.ElementAt(UnityEngine.Random.Range(0, items.Count)).Value;
        // }
    }
}