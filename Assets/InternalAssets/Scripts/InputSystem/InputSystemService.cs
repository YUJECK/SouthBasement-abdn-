using System;
using TheRat;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace TheRat.InputServices
{
    public sealed class InputSystemService : IInputService, ITickable
    {
        public event Action<Vector2> OnMoved;
        public event Action OnInteracted;
        public event Action OnDashed;
        public event Action OnAttack;

        private readonly InputMap _inputActions;

        public InputSystemService()
        {
            _inputActions = new InputMap();

            _inputActions.CharacterContoller.Attack.performed += (context) => OnAttack?.Invoke();
            _inputActions.CharacterContoller.Interaction.performed += (context) => OnInteracted?.Invoke();
            _inputActions.CharacterContoller.Dash.performed += (context) => OnDashed?.Invoke();
            
            _inputActions.Enable();
        }

        public void Tick()
            => OnMoved?.Invoke(GetMovement());
        
        private Vector2 GetMovement() 
            => _inputActions.CharacterContoller.Move.ReadValue<Vector2>();
    }
}