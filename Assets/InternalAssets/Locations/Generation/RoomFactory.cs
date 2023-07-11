using System.Collections.Generic;
using SouthBasement.Helpers;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace SouthBasement.Generation
{
    public sealed class RoomFactory : MonoBehaviour, IRoomFactory
    {
        private DiContainer _diContainer;
        private LevelConfig _levelConfig;

        private Room _owner;
        private Direction _direction;
        private GenerationController _generationController;
        private ContainersHelper _containersHelper;

        [Inject]
        private void Construct(LevelConfig levelConfig, DiContainer diContainer, GenerationController generationController, ContainersHelper containersHelper)
        {
            _diContainer = diContainer;
            _levelConfig = levelConfig;
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

        public Room CreateByType(RoomType roomType, bool remove = true)
        {
            var roomToSpawn = _levelConfig.GetRandomRoomFor(roomType, _direction);
            
            if(remove)
                _levelConfig.Remove(roomToSpawn, roomType);
            
            return CreateByPrefab(roomToSpawn);
        }

        public Room CreateByPrefab(Room roomToSpawn)
        {
            transform.localPosition = GetPosition(roomToSpawn);

            if (!CheckPlace(roomToSpawn))
                return null;

            var spawnedRoom = SpawnRoom(roomToSpawn);

            ConnectRooms(spawnedRoom);

            spawnedRoom.PassageHandler.BuildPassages(new List<Direction> { DirectionHelper.GetOpposite(_direction) });
            
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
            spawnedRoom.PassageHandler.GetPassage(DirectionHelper.GetOpposite(_direction)).Connect(_owner);
            _owner.PassageHandler.GetPassage(_direction).Connect(spawnedRoom);
        }

        private bool CheckPlace(Room roomToSpawn)
        {
            var roomPlaceMask = LayerMask.GetMask("RoomPlace");
            
            var overlapResult = Physics2D.OverlapBoxAll(transform.position, roomToSpawn.RoomSize, 0f, roomPlaceMask);

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