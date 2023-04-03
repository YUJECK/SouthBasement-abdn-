using TheRat.Graphs;
using UnityEngine;

namespace TheRat.LocationGeneration
{
    public class Passage : MonoBehaviour, IGraphEdge
    {
        [field: SerializeField] public Directions Direction { get; private set; }

        private Corridor _corridor;
        private Wall _wall;

        public IGraphVertex EnterVertex { get; set; }
        public IGraphVertex ExitVertex { get; set; }

        private RoomFactory roomFactory;

        private void Awake()
        {
            _corridor = GetComponentInChildren<Corridor>();
            _wall = GetComponentInChildren<Wall>();

            roomFactory = new();
        }

        public void Close()
        {
            _corridor.gameObject.SetActive(false);
            _wall.gameObject.SetActive(true);
        }

        public void Open()
        {
            _corridor.gameObject.SetActive(true);
            _wall.gameObject.SetActive(false);
        }
    }
}