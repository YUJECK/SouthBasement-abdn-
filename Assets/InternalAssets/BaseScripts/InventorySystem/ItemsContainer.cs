using System;
using System.Collections.Generic;
using System.Linq;
using SouthBasement.Helpers;
using SouthBasement.InventorySystem;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace SouthBasement.Items
{
    public sealed class ItemsContainer
    {
        private readonly Dictionary<Rarity, Dictionary<Type, Dictionary<string, InventoryContainer>>> _items;
        private ItemPicker _itemPickerPrefab;

        private DiContainer _diContainer;
        
        public ItemsContainer(Item[] items, DiContainer diContainer)
        {
            _items = new Dictionary<Rarity, Dictionary<Type, Dictionary<string, InventoryContainer>>>()
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
                _items[item.Rarity].TryAdd(item.GetItemType(), new Dictionary<string, InventoryContainer>());
                _items[item.Rarity][item.GetItemType()].TryAdd(item.ItemCategory, new InventoryContainer());
                
                _items[item.Rarity][item.GetItemType()][item.ItemCategory].TryAddItem(item);
            }
        }
        
        public Item Get(string id)
        {
            foreach (var container in _items.SelectMany(itemsRarityContainer
                         => itemsRarityContainer.Value.SelectMany(itemsTypeContainer => itemsTypeContainer.Value)))
            {
                if(container.Value.TryGetItem(id, out var item))
                    return item;
            }

            return null;
        }

        public Item GetRandomInRarity(Rarity rarity)
        {
            var rarityContainers = new List<Item>();

            foreach (var container in _items[rarity]
                         .SelectMany(rarityContainer=> rarityContainer.Value))
            {
                rarityContainers.AddRange(container.Value.GetAllInContainer());
            }

            return rarityContainers.ElementAt(UnityEngine.Random.Range(0, rarityContainers.Count));
        }
        public TItem GetRandomInType<TItem>() where TItem : Item
        {
            Rarity rarity = (Rarity)ChanceSystem.GetChance();

            return _items[rarity][typeof(TItem)].ElementAt(UnityEngine.Random.Range(0, _items[rarity][typeof(TItem)].Count)) as TItem;
        }
        public Item GetRandomInCategory(string category)
        {
            List<Item> items = new List<Item>();

            foreach (var rarityContainerPair in _items)
            {
                Dictionary<Type, Dictionary<string, InventoryContainer>> rarityContainer = rarityContainerPair.Value;
                foreach (KeyValuePair<Type, Dictionary<string, InventoryContainer>> typeContainerPair in rarityContainer)
                {
                    if (typeContainerPair.Value.TryGetValue(category, out var container)) items.AddRange(container.GetAllInContainer());
                }
            }

            return items.ElementAt(UnityEngine.Random.Range(0, items.Count));
        }
        public Item GetRandomInRarityCategory(Rarity rarity, string category)
        {
            Dictionary<Type, Dictionary<string, InventoryContainer>> rarityContainer = _items[rarity];
            List<Item> items = new();

            foreach (var typeContainerPair in rarityContainer)
            {
                if(typeContainerPair.Value.TryGetValue(category, out var container))
                    items.AddRange(container.GetAllInContainer());
            }

            return items.ElementAt(UnityEngine.Random.Range(0, items.Count));
        }
    }
}