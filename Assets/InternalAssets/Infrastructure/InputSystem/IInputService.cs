using System;
using UnityEngine;

namespace SouthBasement.InputServices
{
    public interface IInputService
    {
        event Action<Vector2> OnMoved;
        event Action OnInteracted;
        event Action OnDashed;
        event Action OnAttack;
        event Action ActiveItemUsage;
        event Action InventoryOpen;
        event Action OnMapOpen;
        event Action OnMapClosed;
    }
}   