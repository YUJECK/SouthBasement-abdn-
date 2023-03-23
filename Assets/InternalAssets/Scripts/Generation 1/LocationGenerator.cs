using System;
using System.Collections.Generic;
using TheRat.Helpers;
using UnityEngine;
using Zenject;

namespace TheRat.LocationGeneration
{
    public class LocationGenerator : MonoBehaviour, IGeneratorController
    {
        [SerializeField] private int _roomsCount = 14;
        [SerializeField] private Transform _startPoint;

        public event Action OnGenerationStarted;
        public event Action OnGenerationEnded;

        private RoomsStorager _roomsStorager;
        private ContainersHelper _containersHelper;

        private List<Room> _roomsSpawned;

        [Inject]
        public void Construct(RoomsStorager roomsStorager, ContainersHelper containersHelper)
        {
            this._roomsStorager = roomsStorager;
            this._containersHelper = containersHelper;
        }

        public void StartGeneration()
        {
            Queue<Room> roomsQueue = new();

            roomsQueue.Enqueue(SpawnStartRoom());
            
            foreach(Room room in roomsQueue)
            {
                foreach (RoomFactory factory in room.factories)
                {
                    Room newRoom = factory.Create();

                    roomsQueue.Enqueue(newRoom);
                    _roomsSpawned.Add(newRoom);
                }
            }
        }

        private Room SpawnStartRoom()
        {
            Room randomStartRoom = GetRandomRoom(_roomsStorager.StartRooms);
            Room startRoom = Instantiate(randomStartRoom, _startPoint.position, Quaternion.identity, _containersHelper.RoomsContainer);

            _roomsSpawned.Add(startRoom);

            return startRoom;
        }

        public void Regenerate()
        {
            foreach (RoomFactory roomFactory in _roomsSpawned[0].factories)
                roomFactory.Destroy();
        }

        private Room GetRandomRoom(List<Room> rooms)
        {
            Room randomRoom = RandomizeRoom();

            while (randomRoom.chance >= ChanceSystem.GetChance())
                return randomRoom;

            return null;

            Room RandomizeRoom() => rooms[UnityEngine.Random.Range(0, rooms.Count)];
        }
    }
}