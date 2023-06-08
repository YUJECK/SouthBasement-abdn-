using System;
using UnityEngine;

namespace TheRat.InputServices
{
    public interface IInputService
    {
        event Action<Vector2> OnMoved;
        event Action OnInteracted;
        event Action OnAttack;
    }
}   