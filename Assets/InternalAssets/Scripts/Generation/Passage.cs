using UnityEngine;

namespace TheRat.Generation
{
    public sealed class Passage : MonoBehaviour
    {
        [field: SerializeField] public Vector2 OffCenter { get; private set; } 
        [field: SerializeField] public RoomFactory Factory { get; private set; }
        
        public Room ConnectedRoom { get; private set; }

        public void Connect(Room room)
        {
            ConnectedRoom = room;
        }
    }
}