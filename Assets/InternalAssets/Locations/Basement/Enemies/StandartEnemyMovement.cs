using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace SouthBasement.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class StandartEnemyMovement : MonoBehaviour, IEnemyMovable
    {
        [SerializeField] private AudioSource walkSound;

        private NavMeshAgent _agent;
        private IEnemyMovable _enemyMovable;
        private Coroutine _waitCoroutine;

        public float Speed
        {
            get => _agent.speed;
            set => _agent.speed = value;
        }

        public bool Blocked
        {
            get => _agent.isStopped;
            set => _agent.isStopped = value;
        }

        public Vector2 CurrentMovement => _agent.velocity;

        private void Awake() => _agent = GetComponent<NavMeshAgent>();

        private void Update()
        {
            if(walkSound == null)
                return;

            if(CurrentMovement != Vector2.zero && !walkSound.isPlaying) walkSound.Play();
            else walkSound.Stop();
        }

        public void Move(Vector2 to, Action onCompleted = null)
        {
            if (!Blocked)
            {
                _agent.SetDestination(to);
                
                if (_waitCoroutine != null) StopCoroutine(_waitCoroutine);
                
                if (onCompleted != null)
                    _waitCoroutine = StartCoroutine(WaitForComplete(onCompleted));
            }
        }

        private IEnumerator WaitForComplete(Action onCompleted)
        {
            yield return new WaitUntil(() => Vector2.Distance(_agent.transform.transform.position, _agent.destination) <= _agent.stoppingDistance);
            onCompleted?.Invoke();
        }
    }
}