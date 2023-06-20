using System;
using NTC.GlobalStateMachine;
using SouthBasement.AI;
using SouthBasement.Characters;
using UnityEngine;
using IdleState = NTC.GlobalStateMachine.IdleState;

namespace SouthBasement.Generation
{
    [RequireComponent(typeof(RoomDoorController))]
    public sealed class RoomFightController : MonoBehaviour
    {
        private Enemy[] _enemies;

        private int _enemiesCount;
        public event Action OnEnemiesDefeated;

        private void Awake()
        {
            _enemies = GetComponentsInChildren<Enemy>();

            _enemiesCount = _enemies.Length;
            
            foreach (var enemy in _enemies)
                enemy.OnDied += OnEnemyDied;
            
            GetComponent<PlayerEnterTrigger>().OnEntered += OnEntered;
        }

        private void OnEnemyDied()
        {
            _enemiesCount--;

            if (_enemiesCount <= 0)
            {
                OnEnemiesDefeated?.Invoke();
                GlobalStateMachine.Push<IdleState>();
            }
        }

        private void OnEntered(Character obj)
        {
            foreach (var enemy in _enemies)
            {
                if(enemy != null)
                    enemy.Enable();
            }
            GlobalStateMachine.Push<FightState>();
        }
    }
}