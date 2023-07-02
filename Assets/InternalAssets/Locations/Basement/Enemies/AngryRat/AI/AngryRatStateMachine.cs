using NavMeshPlus.Extensions;
using NTC.ContextStateMachine;
using SouthBasement.Characters.Components;
using SouthBasement.PlayerServices;
using UnityEngine;
using UnityEngine.AI;

namespace SouthBasement.AI
{
    [RequireComponent(typeof(IEnemyMovable))]
    [RequireComponent(typeof(IFlipper))]
    public class AngryRatStateMachine : Enemy
    {
        public EnemyAttacker EnemyAttacker { get; private set; }
        public IFlipper Flipper { get; private set; }
        public EnemyAnimator EnemyAnimator { get; private set; }
        public TargetSelector TargetSelector { get; private set; }
        public AttackTrigger AttackTrigger { get; private set; }
        public IEnemyMovable Movement { get; private set; }

        public bool CurrentAttacking = false;

        protected readonly StateMachine<AngryRatStateMachine> StateMachine = new();

        private void Start()
        {
            EnemyAnimator = new EnemyAnimator(GetComponentInChildren<Animator>());
            
            Movement = GetComponent<IEnemyMovable>();
            Flipper = GetComponent<IFlipper>();
            
            TargetSelector = GetComponentInChildren<TargetSelector>();
            AttackTrigger = GetComponentInChildren<AttackTrigger>();
            EnemyAttacker = GetComponentInChildren<EnemyAttacker>();

            CreateStates();
        }

        protected virtual void CreateStates()
        {
            StateMachine.AddStates(new IdleState(this), new WalkState(this), new AngryRatAttackState(this), new AFKState(this));

            StateMachine.AddAnyTransition<AFKState>(CanEnterAFK);
            StateMachine.AddAnyTransition<AngryRatAttackState>(CanEnterAttackState);
            StateMachine.AddAnyTransition<WalkState>(CanEnterWalkState);
            StateMachine.AddAnyTransition<IdleState>(CanEnterIdleState);

            StateMachine.TransitionsEnabled = true;
        }

        public virtual bool CanEnterAttackState() => AttackTrigger.CanAttack && Enabled;
        public virtual bool CanEnterWalkState() => TargetSelector.Target != null && !AttackTrigger.CanAttack && Enabled && !CurrentAttacking;
        public virtual bool CanEnterIdleState() => TargetSelector.Target == null && !AttackTrigger.CanAttack && !CurrentAttacking;
        public virtual bool CanEnterAFK() => !Enabled;
        
        private void FixedUpdate()
        {
            if(Enabled)
                StateMachine.Run();
        }
    }
}