using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TheRat.Generation
{
    public sealed class GenerationController : IInitializable
    {
        private readonly RoomsContainer _roomsContainer;
        private readonly Transform _startPoint;
        private readonly DiContainer _diContainer;

        private readonly List<Room> _spawnedRooms = new();
        private RoomType[] _map;
        private int CurrentRoomsCount => _spawnedRooms.Count;

        public GenerationController(RoomsContainer roomsContainer, DiContainer diContainer, Transform startPoint)
        {
            _roomsContainer = roomsContainer;
            _diContainer = diContainer;
            _startPoint = startPoint;
        }

        public void AddSpawnedRoom(Room room) 
            => _spawnedRooms.Add(room);

        private void GenerateMap()
        {
            _map = new RoomType[_roomsContainer.RoomsCount];

            _map[0] = RoomType.StartRoom;
            _map[^1] = RoomType.ExitRoom;

            _map[Random.Range(1, _map.Length - 2)] = RoomType.TraderRoom;

            for (int i = 0; i < 4; i++)
            {
                int roomID = Random.Range(1, _map.Length - 2);

                if (_map[roomID] != RoomType.FightRoom)
                    _map[roomID] = RoomType.NPCRoom;
                else
                    i--;
            }
        }
        private void Generate()
        {
            var roomQueue = new Queue<Room>();
            var startRoomPrefab = _roomsContainer.GetRandomRoom(RoomType.StartRoom);

            var startRoom = _diContainer.InstantiatePrefabForComponent<Room>(startRoomPrefab, _startPoint.position, Quaternion.identity, null);

            roomQueue.Enqueue(startRoom);
            
            while (CurrentRoomsCount < _map.Length)
            {
                if (roomQueue.Peek().TryGetFree(out var passage))
                {
                    var room = passage.Factory.Create(_map[CurrentRoomsCount]);

                    if (room != null)
                        roomQueue.Enqueue(room);
                    else
                        roomQueue.Dequeue();
                }
                else
                    roomQueue.Dequeue();
                
                if (roomQueue.Count == 0)
                    break;
            }

            foreach (var room in _spawnedRooms)
                room.CloseAllFree();
        }

        public void Initialize()
        {
            GenerateMap();
            Generate();
        }
    }
}