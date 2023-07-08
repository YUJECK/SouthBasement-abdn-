using System;
using SouthBasement.Helpers;
using UnityEngine;
using Zenject;

namespace SouthBasement.Economy
{
    public sealed class CheeseService
    {
        public int CheeseAmount { get; private set; }

        private readonly CheeseObject _cheesePrefab;
        private readonly DiContainer _container;
        
        public event Action<int> OnCheeseAmountChanged;

        public CheeseService(DiContainer container)
        {
            var config = Resources.Load<CheeseServiceConfig>(ResourcesPathHelper.CheeseServiceConfig);
            CheeseAmount = config.StartCheeseAmount;
            
            _cheesePrefab = config.CheesePrefab;
            _container = container;
        }

        public void AddCheese(int addAmount)
        {
            if(addAmount <= 0)
                return;
            
            CheeseAmount += addAmount;
            OnCheeseAmountChanged?.Invoke(CheeseAmount);
        }

        public bool RemoveCheese(int removeAmount)
        {
            if (CheeseAmount <= removeAmount)
                return false;
            
            CheeseAmount -= removeAmount;
            OnCheeseAmountChanged?.Invoke(CheeseAmount);
            
            return true;
        }
        
        public CheeseObject SpawnCheese(Vector2 postition, int amount)
        {
             var resultCheese = _container.InstantiatePrefabForComponent<CheeseObject>(_cheesePrefab, postition, Quaternion.identity, null);
             resultCheese.SetCheeseValue(amount);

             return resultCheese;
        }
    }
}