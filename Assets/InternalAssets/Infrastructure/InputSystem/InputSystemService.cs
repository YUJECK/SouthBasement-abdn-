using System;
using SouthBasement;
using TheRat;
using UnityEngine;
using Zenject;

namespace SouthBasement.InputServices
{
    public sealed class InputSystemService : IInputService, ITickable
    {
        public event Action<Vector2> OnMoved;
        public event Action OnInteracted;
        public event Action OnDashed;
        public event Action OnAttack;
        public event Action ActiveItemUsage;
        public event Action InventoryOpen;
        public event Action OnMapOpen;
        public event Action OnMapClosed;

        private readonly InputMap _inputActions;

        public InputSystemService()
        {
            _inputActions = new InputMap();

            _inputActions.CharacterContoller.Attack.performed += (context) => OnAttack?.Invoke();
            _inputActions.CharacterContoller.Interaction.performed += (context) => OnInteracted?.Invoke();
            _inputActions.CharacterContoller.Dash.performed += (context) => OnDashed?.Invoke();
            _inputActions.HUDController.ActiveItemUsage.performed += (context) => ActiveItemUsage?.Invoke();
            _inputActions.HUDController.InventoryOpen.performed += (context) => InventoryOpen?.Invoke();
            
            _inputActions.Enable();
        }

        public void Tick()
        {
            OnMoved?.Invoke(GetMovement());

            if (_inputActions.HUDController.MapOpen.IsPressed())
            {
                OnMapOpen?.Invoke();
            }   
            else
            {
                OnMapClosed?.Invoke();
            }
        }

        private Vector2 GetMovement() 
            => _inputActions.CharacterContoller.Move.ReadValue<Vector2>();
    }
}