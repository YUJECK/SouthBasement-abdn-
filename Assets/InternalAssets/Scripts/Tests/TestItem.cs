using System;
using TheRat.InventorySystem;
using TheRat.Characters;
using UnityEngine;
using Zenject;

namespace TheRat.Tests
{
    [CreateAssetMenu]
    public sealed class TestItem : ActiveItem
    {
        private StaminaController _staminaController;

        [Inject]
        private void Construct(StaminaController staminaController)
        {
            _staminaController = staminaController;
        }
        
        public event Action OnUsed;
        
        public override void Use()
        {
            _staminaController.TryDo(10);               
            OnUsed?.Invoke();
        }
    }
}