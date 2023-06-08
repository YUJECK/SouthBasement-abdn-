using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TheRat.Generation
{
    public sealed class Passage : MonoBehaviour
    {
        [field: SerializeField] public RoomFactory Factory { get; private set; }
        [SerializeField] private GameObject wall;
        
        public Room ConnectedRoom { get; private set; }


        public void Connect(Room room)
            => ConnectedRoom = room;

        public void Close()
        {
            wall.SetActive(true);
            gameObject.SetActive(false);
        }
        public void Open()
        {
            wall.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}