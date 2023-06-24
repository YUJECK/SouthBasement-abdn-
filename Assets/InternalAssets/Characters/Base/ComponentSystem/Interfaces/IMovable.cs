using System;
using UnityEngine;

namespace SouthBasement.Characters.Components
{
    public interface IMovable
    {
        event Action<Vector2> OnMoved;
        event Action OnMoveReleased;

        bool CanMove { get; set; }
        Vector2 CurrentMovement { get; }

        void Move(Vector2 movement);
    }
}