using System.Collections.Generic;
using TheRat.Graphs;
using TheRat.Interfaces;
using UnityEngine;

namespace TheRat.LocationGeneration
{
    public class Room : MonoBehaviour, IGraphVertex, ISpawnable
    {
        public List<IGraphEdge> Edges { get; private set; } = new();

        public PassagesController PassagesController { get; private set; }

        [field: SerializeField] public int SpawnChance { get; private set; }

        public virtual void OnSpawned() { }

        public void AddEdge(IGraphEdge edge)
            => Edges.Add(edge);
        public void RemoveEdge(IGraphEdge edge)
            => Edges.Remove(edge);
    }
}