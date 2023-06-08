using System.Collections.Generic;
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

        [Inject]
        private void Construct(RoomsContainer roomsContainer, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _roomsContainer = roomsContainer;
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
            Room roomToSpawn = _roomsContainer.GetRandom();

            transform.localPosition = GetPosition(roomToSpawn);

            var test = Physics2D.OverlapBoxAll(transform.position, roomToSpawn.RoomSize, 0f);

            foreach (var hit2D in test)
            {
                if (hit2D.transform.gameObject.TryGetComponent<Room>(out Room room))
                    return null;
            }
            
            var spawnedRoom = _diContainer.InstantiatePrefabForComponent<Room>(roomToSpawn, transform.position, quaternion.identity, null);

            spawnedRoom.GetPassage(DirectionHelper.GetOpposite(_direction)).Connect(_owner);    
            _owner.GetPassage(_direction).Connect(spawnedRoom);
            
            spawnedRoom.BuildPassages( new List<Direction>() {DirectionHelper.GetOpposite(_direction)});
            
            FindObjectOfType<GenerationController>().AddSpawnedRoom(spawnedRoom);
            
            return spawnedRoom;
        }

        private Vector2 GetPosition(Room roomToSpawn)
        {
            return _owner.GetOffCenter(_direction) - roomToSpawn.GetOffCenter(DirectionHelper.GetOpposite(_direction));
        }
    }
}