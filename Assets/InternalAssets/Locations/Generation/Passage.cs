using UnityEngine;

namespace SouthBasement.Generation 
{
    public sealed class Passage : MonoBehaviour
    {
        [field: SerializeField] public RoomFactory Factory { get; private set; }
        
        [SerializeField] private GameObject wall;
        [SerializeField] private GameObject door;
        
        public Room ConnectedRoom { get; private set; }
        
        public void Connect(Room room)
            => ConnectedRoom = room;

        public void OpenDoor()
        {
            if(door != null)
                door.SetActive(false);
        }

        public void CloseDoor()
        {
            if(door != null)
                door.SetActive(true);
        }

        public void DisablePassage()
        {
            wall.SetActive(true);
            gameObject.SetActive(false);
        }
        public void EnablePassage()
        {
            wall.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}