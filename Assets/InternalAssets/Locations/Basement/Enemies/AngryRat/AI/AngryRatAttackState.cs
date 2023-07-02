using NTC.ContextStateMachine;
using SouthBasement.Basement.Enemies.LittleRat;
using SouthBasement.Enums;

namespace SouthBasement.AI
{
    public sealed class AngryRatAttackState : State<AngryRatStateMachine>
    {
        public AngryRatAttackState(AngryRatStateMachine stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter()
            => Attack();

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

                if (Initializer is LittleRatAI littleRatAI)
                    littleRatAI.CanRunAway = true;
                
                Attack();
            });
        }

        private FacingDirections GetDirectionToTarget()
        {
            if (Initializer.transform.position.x < Initializer.TargetSelector.Target.transform.position.x)
                return FacingDirections.Right;

            return FacingDirections.Left;
        }
    }
}