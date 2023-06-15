namespace TheRat.Generation
{
    public static class DirectionHelper
    {
        public static Direction GetOpposite(Direction direction)
        {
            if (direction == Direction.Down)
                return Direction.Up;
            if (direction == Direction.Up)
                return Direction.Down;
            if (direction == Direction.Right)
                return Direction.Left;
            if (direction == Direction.Left)
                return Direction.Right;
            
            return Direction.Down;
        }
    }
}