using System.Collections.Generic;

namespace TheRat.Graphs
{
    public interface IGraph
    {
        List<IGraphVertex> GraphVertices { get; }

        void AddVertex(IGraphVertex vertex);
        void RemoveVertex(IGraphVertex vertex);
    }
}