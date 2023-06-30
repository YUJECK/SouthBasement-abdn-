using NTC.ContextStateMachine;
using SouthBasement.Enums;
using UnityEngine;

namespace SouthBasement.AI
{
    public sealed class AttackState : State<DefaultRatStateMachine>
    {
        public AttackState(DefaultRatStateMachine stateInitializer) : base(stateInitializer) { }

        public override void OnEnter()
        {
            Attack();
        }

        private void Attack()
        {
            if (!Initializer.CanEnterAttackState())
                return;
            
            Initializer.EnemyAnimator.PlayAttack();
            
            Initializer.Flipper.Blocked = true;
            Initializer.Flipper.Flip(GetDirectionToTarget());
            
            Initializer.CurrentAttacking = true;
            
            Initializer.EnemyAttacker.StartAttack(7, () =>
            {
                Initializer.CurrentAttacking = false;
                Initializer.Flipper.Blocked = false;
                Attack();
            });
        }

        private FacingDirections GetDirectionToTarget()
        {
            Debug.Log(Initializer.transform.position.x < Initializer.TargetSelector.Target.transform.position.x);
            
            if (Initializer.transform.position.x < Initializer.TargetSelector.Target.transform.position.x)
                return FacingDirections.Right;

            return FacingDirections.Left;
        }
    }
}