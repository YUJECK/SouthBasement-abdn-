using System;
using Unity.VisualScripting;
using UnityEngine;

namespace TheRat.Economy
{
    public sealed class CheeseService
    {
        public int CheeseAmount { get; private set; }
        private CheeseObject _cheesePrefab;

        public event Action<int> OnCheeseAmountChanged;

        public CheeseService(CheeseServiceConfig config)
        {
            _cheesePrefab = config.CheesePrefab;
            CheeseAmount = config.StartCheeseAmount;
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
             var resultCheese = GameObject.Instantiate<CheeseObject>(_cheesePrefab, postition, Quaternion.identity);
             resultCheese.SetCheeseValue(amount);

             return resultCheese;
        }
    }
}