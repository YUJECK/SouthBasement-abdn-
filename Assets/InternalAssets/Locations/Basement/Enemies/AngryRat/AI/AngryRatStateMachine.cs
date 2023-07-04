using NTC.ContextStateMachine;
using SouthBasement.Characters.Components;
using UnityEngine;

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
        public bool CurrentAttacking { get; set; }

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
            StateMachine.AddStates(new AngryRatIdleState(this), new AngryRatWalkState(this), new AngryRatAttackState(this), new AngryRatAFKState(this));

            StateMachine.AddAnyTransition<AngryRatAFKState>(CanEnterAFK);
            StateMachine.AddAnyTransition<AngryRatAttackState>(CanEnterAttackState);
            StateMachine.AddAnyTransition<AngryRatWalkState>(CanEnterWalkState);
            StateMachine.AddAnyTransition<AngryRatIdleState>(CanEnterIdleState);
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