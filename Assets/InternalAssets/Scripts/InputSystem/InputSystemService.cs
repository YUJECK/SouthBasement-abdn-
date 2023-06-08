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
        public event Action OnAttack;

        private readonly InputMap _inputActions;

        public InputSystemService()
        {
            _inputActions = new InputMap();

            _inputActions.CharacterContoller.Attack.performed += OnAttackPerformed;
            
            _inputActions.Enable();
        }

        public void Tick()
            => OnMoved?.Invoke(GetMovement());

        private void OnAttackPerformed(InputAction.CallbackContext obj) 
            => OnAttack?.Invoke();

        private Vector2 GetMovement() 
            => _inputActions.CharacterContoller.Move.ReadValue<Vector2>();
    }
}