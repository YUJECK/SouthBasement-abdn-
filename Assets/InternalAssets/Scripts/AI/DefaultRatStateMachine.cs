using NavMeshPlus.Extensions;
using NTC.ContextStateMachine;
using TheRat.AI;
using UnityEngine;
using UnityEngine.AI;

namespace TheRat.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AgentOverride2d))]
    public sealed class DefaultRatStateMachine : MonoBehaviour
    {
        public EnemyAnimator EnemyAnimator { get; private set; }
        public TargetSelector TargetSelector { get; private set; }
        public NavMeshAgent Agent { get; private set; }

        private readonly StateMachine<DefaultRatStateMachine> _stateMachine = new();

        private void Awake()
        {
            EnemyAnimator = new EnemyAnimator(GetComponentInChildren<Animator>());
            
            TargetSelector = GetComponentInChildren<TargetSelector>();
            Agent = GetComponent<NavMeshAgent>();

            IdleState idleState = new(this);
            WalkState walkState = new(this);
            
            _stateMachine.AddStates(idleState, walkState);

            _stateMachine.AddAnyTransition<WalkState>(() => TargetSelector.Target != null);
            _stateMachine.AddTransition<WalkState, IdleState>(() => TargetSelector.Target == null);
            
            _stateMachine.TransitionsEnabled = true;
        }

        private void FixedUpdate()
        {
            _stateMachine.Run();
        }
    }
}