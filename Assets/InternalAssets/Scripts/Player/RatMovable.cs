using UnityEngine;

public class RatMovable : IMovable
{
    public bool CanMove { get; set; }
    public float MoveSpeed { get; private set; }

    private Rigidbody2D rigidbody3;

    public void Move()
    {
    }
}