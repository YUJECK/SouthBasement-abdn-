using System;
using UnityEngine;

namespace TheRat.InputServices
{
    public interface IInputService
    {
        event Action<Vector2> OnMoved;
        event Action OnInteracted;
        event Action OnDashed;
        event Action OnAttack;
        event Action ActiveItemUsage;
        event Action InventoryOpen;
        event Action MapOpen;
    }
}   