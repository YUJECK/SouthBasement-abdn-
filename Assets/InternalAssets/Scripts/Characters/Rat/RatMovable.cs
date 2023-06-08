using System;
using TheRat;
using UnityEngine;

public class RatMovable : IMovable
{
    public bool CanMove { get; set; } = true;

    private readonly Rigidbody2D _rigidbody2d;
    private readonly InputMap _inputs;
    private readonly CharacterStats _characterStats;

    public event Action<Vector2> OnMoved;
    public event Action OnMoveReleased;
    public Vector2 Movement => _inputs.CharacterContoller.Move.ReadValue<Vector2>();

    public RatMovable(InputMap inputs, Rigidbody2D rigidbody2D, CharacterStats characterStats)
    {
        _inputs = inputs;
        _rigidbody2d = rigidbody2D;
        _characterStats = characterStats;
    }

    public void Move()
    {
        if (CanMove)
            _rigidbody2d.velocity = Movement * _characterStats.MoveSpeed;
        else
            _rigidbody2d.velocity = Vector2.zero;
        
        if(Movement != Vector2.zero)
            OnMoved?.Invoke(Movement * _characterStats.MoveSpeed);
        else 
            OnMoveReleased?.Invoke();
    }
}