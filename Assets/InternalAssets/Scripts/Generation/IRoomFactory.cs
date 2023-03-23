namespace TheRat.LocationGeneration
{
    public interface IRoomFactory
    {
        Room Create();
        void Destroy();
    }
}