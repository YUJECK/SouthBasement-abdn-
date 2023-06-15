using System.Collections.Generic;
using System.Linq;
using TheRat.InventorySystem;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace TheRat.Items
{
    public sealed class ItemsContainer
    {
        private readonly Dictionary<Rarity, Dictionary<string, Item>> _items;
        private ItemPicker _itemPickerPrefab;

        private DiContainer _diContainer;
        
        public ItemsContainer(Item[] items, DiContainer diContainer)
        {
            _items = new Dictionary<Rarity, Dictionary<string, Item>>
            {
                {Rarity.Usual, new()},
                {Rarity.Unusual, new()},
                {Rarity.Rare, new()},
                {Rarity.VeryRare, new()},
            };
            
            foreach (var item in items)
                _items[item.Rarity].TryAdd(item.ItemID, item);
        }

        public ItemPicker SpawnItem(string id, Vector3 position)
        {
            var itemSO = ScriptableObject.Instantiate(Get(id));
            var item = _diContainer.InstantiatePrefabForComponent<ItemPicker>(_itemPickerPrefab, position, quaternion.identity, null);
            item.SetItem(itemSO);
            
            return item;
        }
        
        public void Add(Item[] toAdd)
        {
            foreach (var item in toAdd)
                _items[item.Rarity].TryAdd(item.ItemID, item);
        }
        
        public Item Get(string id)
        {
            foreach (var itemsRarityContainer in _items)
            {
                if (itemsRarityContainer.Value.TryGetValue(id, out var value))
                    return value;
            }

            return null;
        }

        public Item GetRandomInRarity(Rarity rarity)
        {
            var items = _items[rarity];
            return items.ElementAt(UnityEngine.Random.Range(0, items.Count)).Value;
        }
    }
}