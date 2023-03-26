using System.Collections.Generic;

namespace TheRat.Graphs
{
    public interface IGraphVertex
    {
        List<IGraphEdge> Edges { get; }

        void AddEdge(IGraphEdge edge);
        void RemoveEdge(IGraphEdge edge);
    }
}