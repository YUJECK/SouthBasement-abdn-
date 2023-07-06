using System;
using System.Collections.Generic;
using System.Linq;
using SouthBasement.Helpers;
using SouthBasement.InventorySystem;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace SouthBasement.Items
{
    public sealed class ItemsContainer
    {
        private readonly Dictionary<Rarity, List<Item>> _itemsInRarity = new();
        private readonly Dictionary<Type, List<Item>> _itemsInType = new();
        private readonly Dictionary<string, List<Item>> _itemsInCategory = new();
        private readonly Dictionary<string, Item> _itemsInID = new();

        private ItemPicker _itemPickerPrefab;
        private TradeItem _tradeItemPrefab;

        private DiContainer _diContainer;

        public ItemsContainer(Item[] items, DiContainer diContainer)
        {
            _itemPickerPrefab = Resources.Load<ItemPicker>(ResourcesPathHelper.ItemPickerPrefab);
            _tradeItemPrefab = Resources.Load<TradeItem>(ResourcesPathHelper.TradeItemPrefab);
            
            Add(items);

            _diContainer = diContainer;
        }

        public ItemPicker SpawnItem(string id, Vector3 position)
        {
            var itemSO = ScriptableObject.Instantiate(Get(id));
            var item = _diContainer.InstantiatePrefabForComponent<ItemPicker>(_itemPickerPrefab, position,
                quaternion.identity, null);

            item.SetItem(itemSO);

            return item;
        }

        public ItemPicker SpawnItem(Item item, Vector3 position, Transform parent = null)
        {
            if (item == null)
            {
                Debug.Log("You tried to spawn null item");
                return null;
            }

            var picker =
                _diContainer.InstantiatePrefabForComponent<ItemPicker>(_itemPickerPrefab, position, quaternion.identity,
                    parent);
            picker.SetItem(item);

            return picker;
        }
        public ItemPicker SpawnForTradeItem(Item item, Vector3 position, int price, Transform parent = null)
        {
            if (item == null)
            {
                Debug.Log("You tried to spawn null item");
                return null;
            }

            var picker =
                _diContainer.InstantiatePrefabForComponent<TradeItem>(_tradeItemPrefab, position, quaternion.identity, parent);

            picker.SetItem(item, price);

            return picker;
        }

        public void Add(IEnumerable<Item> toAdd)
        {
            foreach (var item in toAdd)
                Add(item);
        }

        public void Add(Item item)
        {
            _itemsInRarity.TryAdd(item.Rarity, new());
            _itemsInRarity[item.Rarity].Add(item);

            _itemsInType.TryAdd(item.GetItemType(), new());
            _itemsInType[item.GetItemType()].Add(item);

            foreach (var tag in item.ItemTags)
            {
                _itemsInCategory.TryAdd(tag, new());
                _itemsInCategory[tag].Add(item);
            }

            _itemsInID.TryAdd(item.ItemID, item);
        }

        public Item Get(string id) => _itemsInID[id];

        public Item GetRandomInRarity(Rarity rarity)
            => _itemsInRarity[rarity][Random.Range(0, _itemsInRarity[rarity].Count)];

        public Item GetRandomInType(Type type)
            => _itemsInType[type][Random.Range(0, _itemsInType[type].Count)];

        public Item GetRandomInCategory(string category)
            => _itemsInCategory[category][Random.Range(0, _itemsInCategory[category].Count)];

        public Item GetRandomInRarityAndType(Rarity rarity, Type type)
        {
            Item item = GetRandomInRarity(rarity);

            while(item.GetItemType() != type)
                item = GetRandomInRarity(rarity);

            return item;
        }
        public Item GetRandomInRarityAndTypeAndCategory(Rarity rarity, Type type, string category)
        {
            Item item = GetRandomInRarity(rarity);

            while(item.GetItemType() != type && item.ItemTags.Contains(category))
                item = GetRandomInRarity(rarity);

            return item;
        }
        public Item GetRandomInTypeAndCategory(Type type, string category)
        {
            Item item = GetRandomInType(type);

            while(!item.ItemTags.Contains(category))
                item =  GetRandomInType(type);

            return item;
        }
        public Item GetRandomInRarityAndCategory(Rarity rarity, string category)
        {
            Item item = GetRandomInRarity(rarity);

            while(!item.ItemTags.Contains(category))
                item =  GetRandomInRarity(rarity);

            return item;
        }
    }
}