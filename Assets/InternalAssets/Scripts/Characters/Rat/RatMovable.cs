using System;
using TheRat.InputServices;
using UnityEngine;

namespace TheRat.Characters.Rat
{
    public class RatMovable : IMovable
    {
        public bool CanMove { get; set; } = true;

        private readonly Rigidbody2D _rigidbody2d;
        private readonly IInputService _inputs;
        private readonly CharacterStats _characterStats;

        public event Action<Vector2> OnMoved;
        public event Action OnMoveReleased;

        public RatMovable(IInputService inputs, Rigidbody2D rigidbody2D, CharacterStats characterStats)
        {
            _inputs = inputs;
            _rigidbody2d = rigidbody2D;
            _characterStats = characterStats;

            _inputs.OnMoved += Move;
        }

        ~RatMovable()
        {
            _inputs.OnMoved -= Move;
        }

        public void Move(Vector2 movement)
        {
            if (CanMove)
                _rigidbody2d.velocity = movement * _characterStats.MoveSpeed.Value;
            else
                _rigidbody2d.velocity = Vector2.zero;
        
            if(movement != Vector2.zero)
                OnMoved?.Invoke(movement * _characterStats.MoveSpeed.Value);
            else 
                OnMoveReleased?.Invoke();
        }
    }
}