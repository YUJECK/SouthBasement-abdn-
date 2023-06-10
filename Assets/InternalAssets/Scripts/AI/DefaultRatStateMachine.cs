using System;
using NTC.ContextStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace AutumnForest.AI
{
    public sealed class DefaultRatStateMachine : MonoBehaviour
    {
        public EnemyAnimator EnemyAnimator { get; private set; }
        [field: SerializeField] public Transform[] RandomPoints { get; private set; }
        public NavMeshAgent Agent;

        private readonly StateMachine<DefaultRatStateMachine> _stateMachine = new();

        private void Awake()
        {
            EnemyAnimator = new EnemyAnimator(GetComponent<Animator>());

            IdleState idleState = new(this);
            WalkState walkState = new(this);
            
            _stateMachine.AddStates(idleState, walkState);

            _stateMachine.AddAnyTransition<WalkState>(() => true);
            _stateMachine.TransitionsEnabled = true;
        }

        private void FixedUpdate()
        {
            _stateMachine.Run();
        }
    }
}