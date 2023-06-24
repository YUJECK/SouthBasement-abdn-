using System;
using System.Collections.Generic;
using SouthBasement.Helpers;
using SouthBasement.InventorySystem;
using SouthBasement.Items;
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
        private readonly InventoryContainer _itemsInID = new();

        private ItemPicker _itemPickerPrefab;
        private TradeItem _tradeItemPrefab;

        private DiContainer _diContainer;

        public ItemsContainer(Item[] items, DiContainer diContainer)
        {
            _itemPickerPrefab = Resources.Load<ItemPicker>(ResourcesPathHelper.ItemPickerPrefab);
            _tradeItemPrefab = Resources.Load<TradeItem>(ResourcesPathHelper.TradeItemPrefab);
            
            _itemsInID.Init<Item>();

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

        public ItemPicker SpawnItem(Item item, Vector3 position)
        {
            if (item == null)
            {
                Debug.Log("You tried to spawn null item");
                return null;
            }

            var picker =
                _diContainer.InstantiatePrefabForComponent<ItemPicker>(_itemPickerPrefab, position, quaternion.identity,
                    null);
            picker.SetItem(item);

            return picker;
        }
        public ItemPicker SpawnForTradeItem(Item item, Vector3 position, int price)
        {
            if (item == null)
            {
                Debug.Log("You tried to spawn null item");
                return null;
            }

            var picker =
                _diContainer.InstantiatePrefabForComponent<TradeItem>(_tradeItemPrefab, position, quaternion.identity, null);
            
            picker.SetItem(item);
            picker.Price = price;

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

            _itemsInCategory.TryAdd(item.ItemCategory, new());
            _itemsInCategory[item.ItemCategory].Add(item);

            _itemsInID.TryAddItem(item);
        }

        public Item Get(string id)
        {
            return _itemsInID.GetItem(id);
        }

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

            while(item.GetItemType() != type && item.ItemCategory != category)
                item = GetRandomInRarity(rarity);

            return item;
        }
        public Item GetRandomInTypeAndCategory(Type type, string category)
        {
            Item item = GetRandomInType(type);

            while(item.ItemCategory != category)
                item =  GetRandomInType(type);

            return item;
        }
        public Item GetRandomInRarityAndCategory(Rarity rarity, string category)
        {
            Item item = GetRandomInRarity(rarity);

            while(item.ItemCategory != category)
                item =  GetRandomInRarity(rarity);

            return item;
        }
    }
}