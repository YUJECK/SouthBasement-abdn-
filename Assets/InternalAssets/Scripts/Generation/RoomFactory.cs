using System;
using UnityEngine;
using Zenject;

namespace TheRat.LocationGeneration
{
    public sealed class RoomFactory : MonoBehaviour
    {
        [SerializeField] private Directions _direction;

        public Passage Passage { get; private set; }

        private RoomsStorager _roomsStorager;
        private DiContainer _container;

        private Room _owner;
        private Passage _passage;

        public event Action OnSpawned;
        public event Action OnDestroyed;

        [Inject]
        private void Construct(RoomsStorager roomsStorager, DiContainer container)
        {
            _roomsStorager = roomsStorager;
            _container = container;
        }

        public void SetPassage(Passage passage)
        {
            if (passage != null)
                _passage = passage;
        }

        public Room Create(Passage connectedPassage)
        {
            RoomBuilder roomBuilder = new();

            Room newRoom = InstantiateRoom();

            newRoom.OnSpawned();
            OnSpawned?.Invoke();
            
            roomBuilder.ConnectPassageToExit(newRoom, connectedPassage);

            return newRoom;
        }

        private Room InstantiateRoom()
        {
            Room newRoomPrefab = GetRandomRoom(); 

            Vector2 spawnPosition 
                = _passage.Config.SpawnOffset;

            return _container.InstantiatePrefab(newRoomPrefab, spawnPosition, Quaternion.identity, null)
                .GetComponent<Room>();
        }

        private Room GetRandomRoom()
        {
            return _roomsStorager.GetRandomRoom(_roomsStorager.EnemyRooms);
        }

        public void Destroy()
        {
        }
    }
}