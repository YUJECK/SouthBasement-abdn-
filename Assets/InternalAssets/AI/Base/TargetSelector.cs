using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace SouthBasement.AI
{
    public class TargetSelector : MonoBehaviour
    {
        [SerializeField] private List<string> blackTagList = new List<string>();  
        
        public EnemyTarget Target { get; private set; }
        public List<EnemyTarget> Targets { get; private set; } = new List<EnemyTarget>();

        public event Action<EnemyTarget> OnTargetFound;
        public event Action<EnemyTarget> OnTargetLeft;

        public void SetNewTarget() 
        {
            Target = FindNewTarget();

            if (Target == null)
            {
                OnTargetLeft?.Invoke(Target);
                return;
            }
            if (Target != null)
                OnTargetFound?.Invoke(Target);
        }
        private EnemyTarget FindNewTarget()
        {
            if (Targets.Count == 0)
                return null;
            
            EnemyTarget bestTarget = Targets[0];

            foreach(var nextTarget in Targets)
            {
                if (nextTarget.Priority >= bestTarget.Priority)
                {
                    bestTarget = nextTarget;
                    break;
                }
            }

            return bestTarget;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if(other.isTrigger) return;
            if (blackTagList.Contains(other.tag)) return;
            
            if (other.TryGetComponent(out EnemyTarget newTarget) && !Targets.Contains(newTarget))
            {
                Targets.Add(newTarget);
                SetNewTarget();
            }
        }
        public void OnTriggerExit2D(Collider2D other) 
        {
            if(other.isTrigger)
                return;
            
            if (other.TryGetComponent(out EnemyTarget exitTarget) && Targets.Contains(exitTarget))
            {
                Targets.Remove(exitTarget);
                SetNewTarget();
            }
        }
    }
}