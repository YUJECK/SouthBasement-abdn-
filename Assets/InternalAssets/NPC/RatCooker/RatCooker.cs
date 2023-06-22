using System.Collections.Generic;
using SouthBasement.InventorySystem;
using SouthBasement.Items;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public class RatCooker : MonoBehaviour
    {
        private ItemsContainer _itemsContainer;

        [SerializeField] private Transform[] tradePoints;
        private readonly List<FoodItem> _currentItems = new();

        [Inject]
        private void Construct(ItemsContainer itemsContainer)
         => _itemsContainer = itemsContainer;

        private void Awake()
            => SpawnItems();

        private void SpawnItems()
        {
            foreach (var point in tradePoints)
            {
                var item = GetFood();
                
                _currentItems.Add(item);
                _itemsContainer.SpawnForTradeItem(item, point.position, 8);
            }
        }

        private FoodItem GetFood()
        {
            FoodItem food = _itemsContainer.GetRandomInTypeAndCategory(typeof(FoodItem),"cockroach_food") as FoodItem;
            
            while (_currentItems.Contains(food))
                food = _itemsContainer.GetRandomInTypeAndCategory(typeof(FoodItem),"cockroach_food") as FoodItem;

            return food;
        }
    }
}
