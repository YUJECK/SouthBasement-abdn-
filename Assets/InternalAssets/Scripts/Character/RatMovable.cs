using System;
using TheRat;
using UnityEngine;
using Zenject;

public class RatMovable : IMovable
{
    public bool CanMove { get; set; } = true;

    private Rigidbody2D rigidbody2d;
    [Inject] private InputMap inputs; 
    [Inject] private CharacterStats characterStats; 

    public event Action<Vector2> OnMoved;
    public event Action OnMoveReleased;
    public Vector2 Movement => inputs.CharacterContoller.Move.ReadValue<Vector2>();

    public RatMovable(InputMap inputs, Rigidbody2D rigidbody2D, CharacterStats characterStats)
    {
        this.inputs = inputs;
        this.rigidbody2d = rigidbody2D;
        this.characterStats = characterStats;
    }

    public void Move()
    {
        if (CanMove)
        {
            rigidbody2d.velocity = Movement * characterStats.MoveSpeed;
            OnMoved?.Invoke(Movement * characterStats.MoveSpeed);
        }
        else 
        {
            rigidbody2d.velocity = Vector2.zero;
            OnMoveReleased?.Invoke();
        }
    }
}