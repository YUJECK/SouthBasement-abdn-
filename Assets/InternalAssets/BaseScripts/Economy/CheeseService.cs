using System;
using UnityEngine;
using Zenject;

namespace SouthBasement.Economy
{
    public sealed class CheeseService
    {
        public int CheeseAmount { get; private set; }
        private CheeseObject _cheesePrefab;

        private DiContainer _container;
        
        public event Action<int> OnCheeseAmountChanged;

        public CheeseService(CheeseServiceConfig config, DiContainer container)
        {
            _cheesePrefab = config.CheesePrefab;
            CheeseAmount = config.StartCheeseAmount;
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
            if (CheeseAmount >= removeAmount)
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