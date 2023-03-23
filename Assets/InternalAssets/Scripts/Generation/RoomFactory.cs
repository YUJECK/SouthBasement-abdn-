using TheRat.Helpers;
using UnityEngine;
using Zenject;

namespace TheRat.LocationGeneration
{
    public sealed class RoomFactory : MonoBehaviour, IRoomFactory
    {
        private RoomsStorager _roomsStorager;
        private DiContainer _container;

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
            if (_canSpawn && _spawnedRoom == null)
            {
                _spawnedRoom = _container
                    .InstantiatePrefab(_roomsStorager.GetRandomRoom(_roomsStorager.EnemyRooms), transform.position, Quaternion.identity, null)
                    .GetComponent<Room>();

                return _spawnedRoom;
            }
            else return null;
        }
        public void Destroy()
            => Destroy(_spawnedRoom);
    }
}