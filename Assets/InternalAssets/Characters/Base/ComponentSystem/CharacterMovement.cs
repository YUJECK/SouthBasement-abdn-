using System;
using UnityEngine;

namespace SouthBasement.Characters.Components
{
    public abstract class CharacterMovement<TOwner> : CharacterComponent<TOwner>, IMovable
    {
        public event Action<Vector2> OnMoved;
        public event Action OnMoveReleased;

        protected void InvokeOnMoved(Vector2 movement)
            => OnMoved?.Invoke(movement);

        protected void InvokeOnMovedReleased()
            => OnMoveReleased?.Invoke();

        public bool CanMove { get; set; }

        public Vector2 CurrentMovement { get; protected set; }

        public abstract void Move(Vector2 movement);
    }
}