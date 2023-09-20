using NTC.ContextStateMachine;
using SouthBasement.Basement.Enemies.LittleRat;
using SouthBasement.Helpers;

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
            Initializer.Flipper.Flip(FacingDirectionsHelper
                .GetFacingDirectionTo(Initializer.transform, Initializer.TargetSelector.Target.transform));

            Initializer.CurrentAttacking = true;
            
            Initializer.EnemyAttacker.StartAttack(() =>
            {
                Initializer.CurrentAttacking = false;
                Initializer.Flipper.Blocked = false;

                if (Initializer is LittleRatAI littleRatAI)
                {
                    littleRatAI.CanRunAway = true;
                    return;
                }

                Attack();
            });
        }
    }
}