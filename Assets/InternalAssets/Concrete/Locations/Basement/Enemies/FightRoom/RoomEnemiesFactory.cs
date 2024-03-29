﻿using System;
using System.Collections.Generic;
using SouthBasement.AI;
using SouthBasement.Helpers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace SouthBasement.Generation
{
    public sealed class RoomEnemiesFactory : MonoBehaviour
    {
        [SerializeField] private Enemy[] enemiesPrefabs;
        private List<Transform> _spawnPoints;

        private readonly List<Enemy> _spawnedEnemies = new();
        private DiContainer _diContainer;

        public event Action<Enemy[]> OnEnemiesSpawned;

        [Inject]
        private void Construct(DiContainer diContainer) 
            => _diContainer = diContainer;

        private void Start()
        {
            GetSpawnPoints();
            ProcessEnemiesPrefabs();
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            foreach (var point in _spawnPoints)
                _spawnedEnemies.Add(_diContainer.InstantiatePrefabForComponent<Enemy>(GetRandomEnemy(), point));
            
            OnEnemiesSpawned?.Invoke(_spawnedEnemies.ToArray());
        }

        private void ProcessEnemiesPrefabs()
            => enemiesPrefabs = ChanceSystem.GetInChance(enemiesPrefabs, ChanceSystem.GetRandomChance());

        private Enemy GetRandomEnemy()
            => enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)];

        private void GetSpawnPoints()
        {
            _spawnPoints = new List<Transform>(GetComponentsInChildren<Transform>());
            _spawnPoints.Remove(transform);
        }
    }
}