public interface IMovable
{
    public bool CanMove { get; set; }
    public float MoveSpeed { get; }

    public void Move();
}