using NavMeshPlus.Extensions;
using NTC.ContextStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace SouthBasement.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AgentOverride2d))]
    public sealed class DefaultRatStateMachine : Enemy
    {
        public EnemyAttacker EnemyAttacker { get; private set; }
        public EnemyAnimator EnemyAnimator { get; private set; }
        public TargetSelector TargetSelector { get; private set; }
        public AttackTrigger AttackTrigger { get; private set; }
        public NavMeshAgent Agent { get; private set; }

        public bool CurrentAttacking = false;

        private readonly StateMachine<DefaultRatStateMachine> _stateMachine = new();

        private void Start()
        {
            EnemyAnimator = new EnemyAnimator(GetComponentInChildren<Animator>());
            
            Agent = GetComponent<NavMeshAgent>();
            
            TargetSelector = GetComponentInChildren<TargetSelector>();
            AttackTrigger = GetComponentInChildren<AttackTrigger>();
            EnemyAttacker = GetComponentInChildren<EnemyAttacker>();

            CreateStates();
        }

        private void CreateStates()
        {
            _stateMachine.AddStates(new IdleState(this), new WalkState(this), new AttackState(this), new AFKState(this));

            _stateMachine.AddAnyTransition<AFKState>(CanEnterAFK);
            _stateMachine.AddAnyTransition<AttackState>(CanEnterAttackState);
            _stateMachine.AddAnyTransition<WalkState>(CanEnterWalkState);
            _stateMachine.AddAnyTransition<IdleState>(CanEnterIdleState);

            _stateMachine.TransitionsEnabled = true;
        }

        public bool CanEnterAttackState() => AttackTrigger.CanAttack && Enabled;
        public bool CanEnterWalkState() => TargetSelector.Target != null && !AttackTrigger.CanAttack && Enabled && !CurrentAttacking;
        public bool CanEnterIdleState() => TargetSelector.Target == null && !AttackTrigger.CanAttack && !CurrentAttacking;
        public bool CanEnterAFK() => !Enabled;
        
        private void FixedUpdate()
        {
            if(Enabled)
                _stateMachine.Run();
        }
    }
}