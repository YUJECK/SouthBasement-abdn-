using TheRat.Graphs;
using UnityEngine;

namespace TheRat.LocationGeneration
{
    public class Passage : MonoBehaviour, IGraphEdge
    {
        [field: SerializeField] public PassageConfig Config { get; private set; }

        private Corridor _corridor;
        private Wall _wall;

        public IGraphVertex EnterVertex { get; set; }
        public IGraphVertex ExitVertex { get; set; }

        private void Awake()
        {
            _corridor = GetComponentInChildren<Corridor>();
            _wall = GetComponentInChildren<Wall>();

            if (TryGetComponent<RoomFactory>(out var factory))
                factory.SetPassage(this);
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