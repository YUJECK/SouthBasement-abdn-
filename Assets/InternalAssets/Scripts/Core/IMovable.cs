using System;
using UnityEngine;

public interface IMovable
{
    public event Action<Vector2> OnMoved;
    public event Action OnMoveReleased;

    public bool CanMove { get; set; }
    public float MoveSpeed { get; }

    public void Move();
}