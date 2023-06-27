using System;
using System.Collections.Generic;
using NTC.GlobalStateMachine;
using SouthBasement.AI;
using SouthBasement.Helpers;
using UnityEngine;
using Zenject;
using IdleState = NTC.GlobalStateMachine.IdleState;
using Random = UnityEngine.Random;

namespace SouthBasement.Generation
{
    public sealed class RoomEnemiesFactory : MonoBehaviour
    {
        [SerializeField] private Enemy[] enemiesPrefabs;
        private List<Transform> _spawnPoints;

        private int _enemiesCount;
        private readonly List<Enemy> _spawnedEnemies = new();
        private DiContainer _diContainer;
        
        public event Action OnEnemiesDefeated;

        [Inject]
        private void Construct(DiContainer diContainer) 
            => _diContainer = diContainer;

        private void Start()
        {
            GetSpawnPoints();
            ProcessEnemiesPrefabs();
            SpawnEnemies();
            BindEnemies();
        }

        private void SpawnEnemies()
        {
            foreach (var point in _spawnPoints)
                _spawnedEnemies.Add(_diContainer.InstantiatePrefabForComponent<Enemy>(GetRandomEnemy(), point));
        }

        private void ProcessEnemiesPrefabs()
            => enemiesPrefabs = ChanceSystem.GetInChance(enemiesPrefabs, ChanceSystem.GetChance());

        private Enemy GetRandomEnemy()
            => enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)];

        private void GetSpawnPoints()
        {
            _spawnPoints = new List<Transform>(GetComponentsInChildren<Transform>());
            _spawnPoints.Remove(transform);
        }

        private void BindEnemies()
        {
            _enemiesCount = _spawnedEnemies.Count;

            foreach (var enemy in _spawnedEnemies)
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
            foreach (var enemy in _spawnedEnemies)
            {
                if(enemy != null)
                    enemy.Enable();
            }
        }
    }
}