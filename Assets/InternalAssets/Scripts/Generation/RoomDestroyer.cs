using TheRat.Graphs;
using TheRat.LocationGeneration;
using UnityEngine;

[RequireComponent(typeof(RoomObserver))]
[RequireComponent(typeof(EnemyRoomFactory))]
public class RoomDestroyer : MonoBehaviour
{
    private RoomObserver _roomObserver;
    private EnemyRoomFactory _roomFactory;

    [SerializeField] private Room _thisRoom;

    private void Start()
    {
        _thisRoom = GetComponentInParent<Room>();
        _roomObserver = GetComponent<RoomObserver>();
        _roomFactory = GetComponent<EnemyRoomFactory>();

        _roomObserver.OnRoomEntered += OnRoomEntered;
    }

    private void OnRoomEntered(Room room)
    {
        foreach (IGraphEdge edge in _thisRoom.Edges)
        {
            if (room == (object)edge.EnterVertex || room == (object)edge.ExitVertex)
                return;
        }

        Destroy(_roomFactory.gameObject);
    }
}