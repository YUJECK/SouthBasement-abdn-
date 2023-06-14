using TheRat.Characters;
using UnityEngine;

namespace TheRat.Generation
{
    [RequireComponent(typeof(RoomEnemyController))]
    public sealed class RoomDoorController : MonoBehaviour
    {
        private PlayerEnterTrigger _playerEnterTrigger;
        private Room _room;

        private void Awake()
        {
            _playerEnterTrigger = GetComponent<PlayerEnterTrigger>();
            _playerEnterTrigger.OnEntered += OnEntered;
            
            _room = GetComponentInParent<Room>();
            GetComponent<RoomEnemyController>().OnEnemiesDefeated += () => _room.OpenAllDoors();
        }

        private void OnEntered(Character obj)
        {
            _room.CloseAllDoors();
        }
    }
}