namespace SouthBasement.Generation
{
    public interface IRoomFactory
    {
        void Init(Room owner, Direction direction);
        Room CreateByType(RoomType roomType);
    }
}