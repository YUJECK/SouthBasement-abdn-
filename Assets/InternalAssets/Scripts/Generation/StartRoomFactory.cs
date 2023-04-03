using UnityEngine;
using Zenject;

namespace TheRat.LocationGeneration
{
    public sealed class StartRoomFactory : MonoBehaviour, IRoomFactory
    {
        private RoomsStorager _roomsStorager;
        private DiContainer _container;

        public bool IsSpawned
            => _spawnedRoom != null;

        public Directions Direction { get; private set; }

        public Passage ConnectedPassage => null;

        private Room _spawnedRoom;

        private bool _canSpawn = true;

        [Inject]
        private void Construct(RoomsStorager roomsStorager, DiContainer container)
        {
            _roomsStorager = roomsStorager;
            _container = container;
        }

        public Room Create()
        {
            if (_canSpawn && !IsSpawned)
            {
                _spawnedRoom = InstantiateRoom();
                _spawnedRoom.OnSpawned();

                return _spawnedRoom;
            }
            else return null;
        }

        public void Destroy()
            => Destroy(_spawnedRoom.gameObject);

        private Room InstantiateRoom()
        {
            return _container
                .InstantiatePrefab(_roomsStorager.GetRandomRoom(_roomsStorager.EnemyRooms), transform.position, Quaternion.identity, null)
                .GetComponent<Room>();
        }
    }
}