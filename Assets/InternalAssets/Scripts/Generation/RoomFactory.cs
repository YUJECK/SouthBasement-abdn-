using System.Collections.Generic;
using TheRat.Helpers;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace TheRat.Generation
{
    public sealed class RoomFactory : MonoBehaviour
    {
        private DiContainer _diContainer;
        private RoomsContainer _roomsContainer;

        private Room _owner;
        private Direction _direction;
        private GenerationController _generationController;
        private ContainersHelper _containersHelper;

        [Inject]
        private void Construct(RoomsContainer roomsContainer, DiContainer diContainer, GenerationController generationController, ContainersHelper containersHelper)
        {
            _diContainer = diContainer;
            _roomsContainer = roomsContainer;
            _generationController = generationController;
            _containersHelper = containersHelper;
        }

        public void Init(Room owner, Direction direction)
        {
            if (_owner == null)
            {
                _owner = owner;
                _direction = direction;
            }
        }
        
        public Room Create()
        {
            var roomToSpawn = _roomsContainer.GetRandomStart();

            transform.localPosition = GetPosition(roomToSpawn);

            if (!CanSpawn(roomToSpawn))
                return null;

            var spawnedRoom = SpawnRoom(roomToSpawn);

            ConnectRooms(spawnedRoom);

            spawnedRoom.BuildPassages(new List<Direction> { DirectionHelper.GetOpposite(_direction) });
            
            _generationController.AddSpawnedRoom(spawnedRoom);
            
            return spawnedRoom;
        }

        private Room SpawnRoom(Room roomToSpawn)
        {
            var spawnedRoom = _diContainer
                .InstantiatePrefabForComponent<Room>(roomToSpawn, transform.position, quaternion.identity,
                    _containersHelper.RoomsContainer);
            return spawnedRoom;
        }

        private void ConnectRooms(Room spawnedRoom)
        {
            spawnedRoom.GetPassage(DirectionHelper.GetOpposite(_direction)).Connect(_owner);
            _owner.GetPassage(_direction).Connect(spawnedRoom);
        }

        private bool CanSpawn(Room roomToSpawn)
        {
            var overlapResult = Physics2D.OverlapBoxAll(transform.position, roomToSpawn.RoomSize, 0f);

            foreach (var hit2D in overlapResult) 
            {
                if (hit2D.transform.gameObject.TryGetComponent(out Room room) && room != _owner)
                    return false;
            }

            return true;
        }

        private Vector2 GetPosition(Room roomToSpawn) 
            => _owner.GetOffCenter(_direction) - roomToSpawn.GetOffCenter(DirectionHelper.GetOpposite(_direction));
    }
}