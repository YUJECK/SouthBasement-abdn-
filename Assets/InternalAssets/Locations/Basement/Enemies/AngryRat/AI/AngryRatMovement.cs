using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace SouthBasement.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class AngryRatMovement : MonoBehaviour, IEnemyMovable
    {
        [SerializeField] private NavMeshAgent agent;
        
        private IEnemyMovable _enemyMovableImplementation;
        private Coroutine _waitCoroutine;

        public bool Blocked { get; set; }
        public Vector2 CurrentMovement => agent.velocity;

        private void Awake() => agent = GetComponent<NavMeshAgent>();

        public void Move(Vector2 to, Action onCompleted = null)
        {
            if (!Blocked) agent.SetDestination(to);

            if (onCompleted != null)
            {
                if (_waitCoroutine != null)
                {
                    StopCoroutine(_waitCoroutine);
                    Debug.Log("Stop");
                }
                
                _waitCoroutine = StartCoroutine(WaitForComplete(onCompleted));
            }
        }

        private IEnumerator WaitForComplete(Action onCompleted)
        {
            yield return new WaitUntil(() => agent.remainingDistance <= agent.stoppingDistance);
            onCompleted?.Invoke();
        }
    }
}