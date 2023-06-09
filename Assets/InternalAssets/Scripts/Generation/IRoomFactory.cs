namespace TheRat.Generation
{
    public interface IRoomFactory
    {
        void Init(Room owner, Direction direction);
        Room Create(RoomType roomType);
    }
}