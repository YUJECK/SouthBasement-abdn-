using System.Collections.Generic;
using SouthBasement.AI;
using SouthBasement.Helpers.Rotator;
using SouthBasement.Scripts.Helpers;
using UnityEngine;

namespace SouthBasement
{
    public sealed class CarrotDroneTargetController : MonoBehaviour 
    {
        public Transform CurrentTarget { get; private set; }

        private TriggerCallback _triggerCallback;
        private readonly List<Transform> _currentTargets = new();
        
        private void Awake()
        {
            _triggerCallback = GetComponentInChildren<TriggerCallback>();
            
            _triggerCallback.OnTriggerEnter += OnEnter;
            _triggerCallback.OnTriggerExit += OnExit;
        }

        private void OnDestroy()
        {
            _triggerCallback.OnTriggerEnter -= OnEnter;
            _triggerCallback.OnTriggerExit -= OnExit;
        }

        private void OnEnter(Collider2D targetToCheck)
        {
            if (targetToCheck.TryGetComponent(out EnemyHealth enemyHealth))
            {
                _currentTargets.Add(targetToCheck.transform);
                UpdateTarget();
            }
        }

        private void UpdateTarget()
        {
            if (_currentTargets.Count == 0)
            {
                CurrentTarget = null;
                return;    
            }
            
            var bestTarget = _currentTargets[0];
            
            foreach (var target in _currentTargets)
            {
                if (CheckDistance(bestTarget, target))
                    bestTarget = target;
            }

            CurrentTarget = bestTarget;
            GetComponent<ObjectRotator>().Target = CurrentTarget;
        }

        private bool CheckDistance(Transform bestTarget, Transform target)
        {
            var position = transform.position;
            
            return Vector2.Distance(position, bestTarget.position) 
                   > Vector2.Distance(position, target.position);
        }

        private void OnExit(Collider2D targetToCheck)
        {
            if(targetToCheck.TryGetComponent(out EnemyHealth enemyHealth))
                _currentTargets.Remove(targetToCheck.transform);
            
            UpdateTarget();
        }
    }
}