using System;
using UnityEngine;
using Zenject;

namespace TheRat.LocationGeneration
{
    public sealed class RoomFactory
    {
        private RoomsStorager _roomsStorager;
        private DiContainer _container;
        
        public bool IsSpawned => _spawnedRoom != null;

        private Room _spawnedRoom;

        public event Action OnSpawned;
        public event Action OnDestroyed;

        [Inject]
        private void Construct(RoomsStorager roomsStorager, DiContainer container)
        {
            _roomsStorager = roomsStorager;
            _container = container;
        }

        public Room Create(Vector2 spawnPosition, Room owner, Passage connectedPassage)
        {
            if (IsSpawned)
                return null;

            RoomBuilder roomBuilder = new();

            Room newRoom = InstantiateRoom(spawnPosition);

            newRoom.OnSpawned();
            OnSpawned?.Invoke();
            
            roomBuilder.ConnectPassageToExit(_spawnedRoom, connectedPassage);

            return _spawnedRoom = newRoom;
        }
        
        public void Destroy()
        {
            if(_spawnedRoom != null)
            {
                GameObject.Destroy(_spawnedRoom.gameObject);
                OnDestroyed?.Invoke();
            }
        }

        private Room InstantiateRoom(Vector2 spawnPosition)
        {
            return _container
                .InstantiatePrefab(_roomsStorager.GetRandomRoom(_roomsStorager.EnemyRooms), spawnPosition, Quaternion.identity, null)
                .GetComponent<Room>();
        }
    }
}