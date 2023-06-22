using System;
using NTC.GlobalStateMachine;
using SouthBasement.AI;
using SouthBasement.Characters;
using UnityEngine;
using IdleState = NTC.GlobalStateMachine.IdleState;

namespace SouthBasement.Generation
{
    public sealed class RoomEnemiesHandler : MonoBehaviour
    {
        private Enemy[] _enemies;

        private int _enemiesCount;
        public event Action OnEnemiesDefeated;

        private void Awake()
            => BindEnemies();

        private void BindEnemies()
        {
            _enemies = GetComponentsInChildren<Enemy>();

            _enemiesCount = _enemies.Length;

            foreach (var enemy in _enemies)
                enemy.OnDied += OnEnemyDied;
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

        public void EnableEnemies()
        {
            foreach (var enemy in _enemies)
            {
                if(enemy != null)
                    enemy.Enable();
            }
        }
    }
}