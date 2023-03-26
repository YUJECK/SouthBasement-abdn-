namespace TheRat.LocationGeneration
{
    public interface IRoomFactory
    {
        Directions Direction { get; }

        Room Create();
        void Destroy();
    }
}