using System.Collections.Generic;
using TheRat.Graphs;

namespace TheRat.LocationGeneration
{
    public sealed class Location : IGraph
    {
        public List<IGraphVertex> GraphVertices { get; private set; }

        public void AddVertex(IGraphVertex vertex) { }
        public void RemoveVertex(IGraphVertex vertex) { }
    }
}