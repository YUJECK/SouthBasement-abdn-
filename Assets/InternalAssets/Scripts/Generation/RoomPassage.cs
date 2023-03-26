using TheRat.Graphs;

namespace TheRat.LocationGeneration
{
    public sealed class RoomPassage : IGraphEdge
    {
        public IGraphVertex EnterVertex { get; private set; }
        public IGraphVertex ExitVertex { get; private set; }
    }
}