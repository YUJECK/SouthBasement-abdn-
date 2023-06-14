using System;
using TheRat.InputServices;
using TheRat.InternalAssets.Scripts.Characters;
using UnityEngine;

namespace TheRat.Characters.Rat
{
    public class RatMovable : IMovable
    {
        public bool CanMove { get; set; } = true;
        public Vector2 Movement { get; private set; }

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
            Movement = movement * _characterStats.MoveSpeed.Value;
            
            if (CanMove)
            {
                _rigidbody2d.velocity = Movement;
            }
            else
            {
                _rigidbody2d.velocity = Vector2.zero;
            }

            if (movement != Vector2.zero)
            {
                OnMoved?.Invoke(Movement);
            }
            else
            {
                OnMoveReleased?.Invoke();
            }
        }
    }
}