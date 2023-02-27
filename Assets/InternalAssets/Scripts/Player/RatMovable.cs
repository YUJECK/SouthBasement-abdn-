using System;
using TheRat;
using UnityEngine;
using Zenject;

public class RatMovable : IMovable
{
    public bool CanMove { get; set; } = true;
    public float MoveSpeed { get; private set; }

    private Rigidbody2D rigidbody;
    private InputMap inputs;

    public event Action<Vector2> OnMoved;
    public event Action OnMoveReleased;
    public Vector2 Movement => inputs.CharacterContoller.Move.ReadValue<Vector2>();

    public RatMovable(Rigidbody2D rigidbody) => this.rigidbody = rigidbody;

    [Inject]
    private void Construct(InputMap inputs)
    {
        this.inputs = inputs;
    }

    public void Move()
    {
        if (CanMove)
        {
            rigidbody.velocity = Movement * MoveSpeed;
            OnMoved?.Invoke(Movement * MoveSpeed);
        }
        else 
        {
            rigidbody.velocity = Vector2.zero;
            OnMoveReleased?.Invoke();
        }
    }
}