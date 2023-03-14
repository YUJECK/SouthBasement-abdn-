using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    public event Action<Vector2> OnMoved;
    public event Action OnAttacked;
    public event Action OnDashed;
}