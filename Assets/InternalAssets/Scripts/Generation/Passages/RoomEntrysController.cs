using TheRat.LocationGeneration;
using UnityEngine;

[RequireComponent(typeof(RoomEntriesContainer))]
public class RoomEntrysController : MonoBehaviour
{
    private RoomEntriesContainer _roomEntriesContainer;

    private void Awake()
         => _roomEntriesContainer = GetComponent<RoomEntriesContainer>();

    public Entry Get(Directions direction)
        => _roomEntriesContainer.Get(direction);

    public void Close(Directions direction)
        => _roomEntriesContainer.Get(direction).ClosePassage();
    public void Open(Directions direction)
        => _roomEntriesContainer.Get(direction).OpenPassage();

    public void Spawn(Directions direction)
        => _roomEntriesContainer.Get(direction).SpawnRoom();
}