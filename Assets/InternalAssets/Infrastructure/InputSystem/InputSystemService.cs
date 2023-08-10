using System;
using SouthBasement;
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
        public event Action OnPaused;

        private readonly InputMap _inputActions;

        public InputSystemService()
        {
            _inputActions = new InputMap();

            _inputActions.CharacterContoller.Attack.performed += _ => OnAttack?.Invoke();
            _inputActions.CharacterContoller.Interaction.performed += _ => OnInteracted?.Invoke();
            _inputActions.CharacterContoller.Dash.performed += _ => OnDashed?.Invoke();
            _inputActions.HUDController.ActiveItemUsage.performed += _ => ActiveItemUsage?.Invoke();
            _inputActions.HUDController.InventoryOpen.performed += _ => InventoryOpen?.Invoke();
            _inputActions.HUDController.Paused.performed += _ => OnPaused?.Invoke();
            
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