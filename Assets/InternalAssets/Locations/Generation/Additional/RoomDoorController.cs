using SouthBasement.Characters;
using UnityEngine;

namespace SouthBasement.Generation
{
    [RequireComponent(typeof(RoomFightController))]
    public sealed class RoomDoorController : MonoBehaviour
    {
        private PlayerEnterTrigger _playerEnterTrigger;
        private Room _room;

        private void Awake()
        {
            _playerEnterTrigger = GetComponent<PlayerEnterTrigger>();
            _playerEnterTrigger.OnEntered += OnEntered;
            
            _room = GetComponentInParent<Room>();
            GetComponent<RoomFightController>().OnEnemiesDefeated += () => _room.OpenAllDoors();
        }

        private void OnEntered(Character obj)
        {
            _room.CloseAllDoors();
        }
    }
}