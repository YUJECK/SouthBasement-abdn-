using System;
using NTC.ContextStateMachine;
using SouthBasement.AI;
using SouthBasement.Helpers.Rotator;
using UnityEngine;
using UnityEngine.UI;

namespace SouthBasement.Basement.Enemies.ArmouredRat.AI
{
    public sealed class ArmouredRatAI : Enemy
    {
        private StateMachine<ArmouredRatAI> _stateMachine;

        public IEnemyMovable EnemyMovable { get; private set; }
        public ArmouredRatAnimator EnemyAnimator { get; private set; }
        public TargetSelector TargetSelector { get; private set; }
        public EnemyAttacker EnemyAttacker { get; private set; }
        public EnemyMovementFlipper Flipper { get; private set; }
        public AttackTrigger AttackTrigger { get; private set; }
        public ArmouredRatDefender ArmouredRatDefender { get; private set; }

        public bool CurrentlyAttacking { get; set; }
        public bool NeedToAttack { get; set; }
        public bool NeedToDefend { get; set; } = true;

        private void Awake()
        {
            InitComponents();
            CreateStates();
        }

        private void Update()
        {
            if(Enabled)
                _stateMachine.Run();
        }

        private void InitComponents()
        {
            ArmouredRatDefender = GetComponentInChildren<ArmouredRatDefender>();
            EnemyMovable = GetComponent<IEnemyMovable>();
            EnemyAnimator = new ArmouredRatAnimator(GetComponentInChildren<Animator>());
            Flipper = GetComponent<EnemyMovementFlipper>();
            TargetSelector = GetComponentInChildren<TargetSelector>();
            EnemyAttacker = GetComponentInChildren<EnemyAttacker>();
            AttackTrigger = GetComponentInChildren<AttackTrigger>();
        }

        private void CreateStates()
        {
            _stateMachine = new StateMachine<ArmouredRatAI>();
            
            _stateMachine.AddStates(new ArmouredRatWalkState(this), new ArmouredRatAttackState(this), new ArmouredRatIdleState(this), new ArmouredRatDefendState(this));

            _stateMachine.AddAnyTransition<ArmouredRatAttackState>(CanEnterAttackState);
            _stateMachine.AddAnyTransition<ArmouredRatWalkState>(CanEnterMoveState);
            _stateMachine.AddAnyTransition<ArmouredRatIdleState>(CanEnterIdleState);
            _stateMachine.AddAnyTransition<ArmouredRatDefendState>(CanEnterDefendState);
        }

        public bool CanEnterDefendState() => Enabled && ArmouredRatDefender.CanDefends() && !CurrentlyAttacking && !NeedToAttack && NeedToDefend;
        public bool CanEnterAttackState() => Enabled && AttackTrigger.CanAttack && !ArmouredRatDefender.CurrentDefending && NeedToAttack && !NeedToDefend;
        public bool CanEnterMoveState() => Enabled && TargetSelector.Target != null && !CurrentlyAttacking && !ArmouredRatDefender.CurrentDefending && !CanEnterDefendState();
        public bool CanEnterIdleState() => Enabled && TargetSelector.Target == null && !CurrentlyAttacking && !ArmouredRatDefender.CurrentDefending;
    }
}