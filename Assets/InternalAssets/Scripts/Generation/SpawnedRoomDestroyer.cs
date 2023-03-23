using TheRat.LocationGeneration;
using UnityEngine;

[RequireComponent(typeof(RoomObserver))]
[RequireComponent(typeof(RoomFactory))]
public class SpawnedRoomDestroyer : MonoBehaviour
{
    private RoomObserver _roomObserver;
    private RoomFactory _roomFactory;

    private void Start()
    {
        _roomObserver = GetComponent<RoomObserver>();
        _roomFactory = GetComponent<RoomFactory>();

        _roomObserver.OnRoomEntered += OnRoomEntered;
    }

    private void OnRoomEntered(Room room)
    {
        _roomFactory.Destroy();
    }
}