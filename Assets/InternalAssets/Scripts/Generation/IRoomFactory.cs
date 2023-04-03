namespace TheRat.LocationGeneration
{
    public interface IRoomFactory
    {
        Directions Direction { get; }
        Passage ConnectedPassage { get; }

        Room Create();
        void Destroy();
    }
}