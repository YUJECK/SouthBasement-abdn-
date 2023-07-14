using System;
using System.Collections.Generic;
using NTC.GlobalStateMachine;
using SouthBasement.AI;
using SouthBasement.AI.MovePoints;
using UnityEngine;
using IdleState = NTC.GlobalStateMachine.IdleState;

namespace SouthBasement.Generation
{
    [RequireComponent(typeof(RoomEnemiesFactory))]
    public sealed class EnemiesHandler : MonoBehaviour
    {
        [SerializeField] private Transform movePointsContainer;
        
        private int _enemiesCount;
        private readonly List<Enemy> _currentEnemies = new();
        private List<MovePoint> _movePoints = new();
        
        public event Action OnEnemiesDefeated;
        public event Action OnEnemyDied;

        private void Awake()
        {
            GetComponent<RoomEnemiesFactory>().OnEnemiesSpawned += BindEnemies;
            _movePoints = new(movePointsContainer.GetComponentsInChildren<MovePoint>());
        }

        public bool IsEnemyCategoryAlone<TEnemy>() 
            where TEnemy : Enemy
        {
            return !_currentEnemies.Exists(enemy => enemy is not TEnemy);
        }
        public bool Contains<TEnemy>() 
            where TEnemy : Enemy
        {
            return _currentEnemies.Exists(enemy => enemy is TEnemy);
        }

        public TEnemy[] Get<TEnemy>()
            where TEnemy : Enemy
        {
            return _currentEnemies.FindAll(enemy => enemy is TEnemy).ToArray() as TEnemy[];
        }
        
        public void DisableEnemies()
        {
            foreach (var pair in _currentEnemies)
            {
                if(pair != null)
                    pair.Disable();
            }
        }
        public void EnableEnemies()
        {
            foreach (var pair in _currentEnemies)
            {
                if(pair != null)
                    pair.Enable();
            }
        }

        private void BindEnemies(Enemy[] enemies)
        {
            _enemiesCount = enemies.Length;

            foreach (var enemy in enemies)
            {
                _currentEnemies.Add(enemy);
                enemy.OnDied += HandlEnemyDeath;
                
                if(enemy is IEnemiesHandlerRequire enemiesHandlerRequire) enemiesHandlerRequire.Initialize(this);
                if(enemy is IMovePointsRequire movePointsRequire) movePointsRequire.Initialize(new MovePointsHandler(_movePoints.ToArray()));
                
                enemy.Disable();
            }
        }

        private void HandlEnemyDeath(Enemy enemy)
        {
            _enemiesCount--;
            OnEnemyDied?.Invoke();

            if (_enemiesCount <= 0)
            {
                OnEnemiesDefeated?.Invoke();
                GlobalStateMachine.Push<IdleState>();
            }

            _currentEnemies.Remove(enemy);
        }
    }
}