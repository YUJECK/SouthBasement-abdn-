namespace TheRat.Graphs
{
    public interface IGraphEdge
    {
        IGraphVertex EnterVertex { get; }
        IGraphVertex ExitVertex { get; }
    }
}