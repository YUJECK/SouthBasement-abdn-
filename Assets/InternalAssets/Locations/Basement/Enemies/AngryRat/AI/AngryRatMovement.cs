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

        public bool Blocked { get; set; }
        public Vector2 CurrentMovement => agent.velocity;

        private void Awake() => agent = GetComponent<NavMeshAgent>();

        public void Move(Vector2 to, Action onCompleted = null)
        {
            if (!Blocked)
                agent.SetDestination(to);

            if (onCompleted != null)
                StartCoroutine(WaitForComplete(onCompleted));
        }

        private IEnumerator WaitForComplete(Action onCompleted)
        {
            while (agent.pathStatus != NavMeshPathStatus.PathComplete)
                yield return null;
            
            onCompleted?.Invoke();
        }
    }
}