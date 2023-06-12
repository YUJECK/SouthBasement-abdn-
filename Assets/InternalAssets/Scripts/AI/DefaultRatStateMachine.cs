using System;
using NavMeshPlus.Extensions;
using NTC.ContextStateMachine;
using TheRat.AI;
using TheRat.InternalAssets.Scripts.Characters;
using UnityEngine;
using UnityEngine.AI;

namespace TheRat.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AgentOverride2d))]
    public sealed class DefaultRatStateMachine : Enemy
    {
        [field: SerializeField] public SingleAnimationService WarningPoint { get; private set; }
        [field: SerializeField] public AttackRangeAnimator AttackRangeAnimator { get; private set; }
        [field: SerializeField] public Transform AttackPoint { get; private set; }
        
        public EnemyAnimator EnemyAnimator { get; private set; }
        public TargetSelector TargetSelector { get; private set; }
        public AttackTrigger AttackTrigger { get; private set; }
        public NavMeshAgent Agent { get; private set; }

        private readonly StateMachine<DefaultRatStateMachine> _stateMachine = new();

        private void Awake()
        {
            EnemyAnimator = new EnemyAnimator(GetComponentInChildren<Animator>());
            
            TargetSelector = GetComponentInChildren<TargetSelector>();
            AttackTrigger = GetComponentInChildren<AttackTrigger>();
            Agent = GetComponent<NavMeshAgent>();

            CreateStates();
        }

        private void CreateStates()
        {
            _stateMachine.AddStates(new IdleState(this), new WalkState(this), new AttackState(this));

            _stateMachine.AddAnyTransition<AttackState>(CanEnterAttackState);
            _stateMachine.AddAnyTransition<WalkState>(CanEnterWalkState);
            _stateMachine.AddAnyTransition<IdleState>(CanEnterIdleState);

            _stateMachine.TransitionsEnabled = true;
        }

        public bool CanEnterAttackState() => AttackTrigger.CanAttack && Enabled;
        public bool CanEnterWalkState() => TargetSelector.Target != null && !AttackTrigger.CanAttack && Enabled;
        public bool CanEnterIdleState() => TargetSelector.Target == null && !AttackTrigger.CanAttack;
        
        private void FixedUpdate()
        {
            _stateMachine.Run();
        }
    }
}