using TheRat.Graphs;
using TheRat.LocationGeneration;
using UnityEngine;

[RequireComponent(typeof(RoomObserver))]
[RequireComponent(typeof(RoomFactory))]
public class RoomDestroyer : MonoBehaviour
{
    private RoomObserver _roomObserver;
    private RoomFactory _roomFactory;

    [SerializeField] private Room _thisRoom;

    private void Start()
    {
        _thisRoom = GetComponentInParent<Room>();
        _roomObserver = GetComponent<RoomObserver>();
        _roomFactory = GetComponent<RoomFactory>();

        _roomObserver.OnRoomEntered += OnRoomEntered;
    }

    private void OnRoomEntered(Room room)
    {
        foreach (IGraphEdge edge in _thisRoom.Edges)
        {
            if (room == (Room)edge.EnterVertex || room == (Room)edge.ExitVertex)
                return;
        }

        Destroy(_thisRoom);
    }
}