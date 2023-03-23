using UnityEngine;
using Zenject;

namespace TheRat.LocationGeneration
{
    public sealed class RoomFactory : MonoBehaviour
    {
        private RoomsStorager _roomsStorager;
        private DiContainer _container;

        [Inject]
        public void Construct(RoomsStorager roomsStorager, DiContainer container)
        {
            _roomsStorager = roomsStorager;
            _container = container;
        }


        public Room Create() 
        {
            return 
                _container.InstantiatePrefab(_roomsStorager.GetRandomRoom(_roomsStorager.EnemyRooms), transform.position, Quaternion.identity, null)
                .GetComponent<Room>();                
        }
        public void Destroy() { }
    }
}