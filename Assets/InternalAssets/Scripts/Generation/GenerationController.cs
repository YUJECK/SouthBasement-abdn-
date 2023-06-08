using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TheRat.Generation
{
    public sealed class GenerationController :  MonoBehaviour
    {
        [SerializeField] private Transform _startPosition;
        
        private RoomsContainer _roomsContainer;
        private DiContainer _diContainer;

        private readonly List<Room> _spawnedRooms = new();
        private int CurrentRoomsCount => _spawnedRooms.Count;

        [Inject]
        private void Construct(RoomsContainer roomsContainer, DiContainer diContainer)
        {
            _roomsContainer = roomsContainer;
            _diContainer = diContainer;
        }

        private void Start()
        {
            Generate();
        }

        public void AddSpawnedRoom(Room room)
        {
            _spawnedRooms.Add(room);
        }
        
        private void Generate()
        {
            var roomQueue = new Queue<Room>();
            var startRoomPrefab = _roomsContainer.GetRandom();

            var startRoom = _diContainer.InstantiatePrefabForComponent<Room>(startRoomPrefab, _startPosition.position, Quaternion.identity, transform);

            roomQueue.Enqueue(startRoom);
            
            while (CurrentRoomsCount <= 20)
            {
                if (roomQueue.Peek().TryGetFree(out var passage))
                {
                    var room = passage.Factory.Create();

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
            {
                room.CloseAllFree();
            }
        }
    }
}