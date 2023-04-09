using UnityEngine;

namespace TheRat.LocationGeneration
{
    public class RoomProvider : MonoBehaviour

    {
        public Room Room { get; private set; }
        public FactoryMixer FactoryMixer { get; private set; }
    }
}