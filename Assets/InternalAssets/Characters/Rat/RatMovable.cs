using System;
using SouthBasement.InputServices;
using SouthBasement.Characters.Stats;
using UnityEngine;

namespace SouthBasement.Characters.Rat
{
    public class RatMovable : IMovable, IDisposable
    {
        public bool CanMove { get; set; } = true;
        public Vector2 Movement { get; private set; }

        private readonly Rigidbody2D _rigidbody2d;
        private readonly IInputService _inputs;
        private readonly CharacterMoveStats _moveStats;

        public event Action<Vector2> OnMoved;
        public event Action OnMoveReleased;

        public RatMovable(IInputService inputs, Rigidbody2D rigidbody2D, CharacterMoveStats moveStats)
        {
            _inputs = inputs;
            _rigidbody2d = rigidbody2D;
            _moveStats = moveStats;

            _inputs.OnMoved += Move;
        }

        public void Dispose()
        {
            _inputs.OnMoved -= Move;
        }

        public void Move(Vector2 movement)
        {
            Movement = movement * _moveStats.MoveSpeed;
            
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