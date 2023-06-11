using TheRat.Player;
using UnityEngine;

namespace TheRat.Generation
{
    public sealed class RoomDoorController : MonoBehaviour
    {
        private PlayerEnterTrigger _playerEnterTrigger;
        private Room _room;

        private void Awake()
        {
            _playerEnterTrigger = GetComponent<PlayerEnterTrigger>();
            _playerEnterTrigger.OnEntered += OnEntered;
            
            _room = GetComponentInParent<Room>();
        }

        private void OnEntered(Character obj)
        {
            _room.CloseAllDoors();
        }
    }
}