namespace TheRat.LocationGeneration
{
    public sealed class RoomBuilder
    {
        public RoomBuilder ConnectPassageToEnter(Room room, Passage connectedPassage)
        {
            connectedPassage.EnterVertex = room;
            room.AddEdge(connectedPassage);

            return this;
        }

        public RoomBuilder ConnectPassageToExit(Room room, Passage connectedPassage)
        {
            connectedPassage.ExitVertex = room;
            room.AddEdge(connectedPassage);

            return this;
        }
    }
}