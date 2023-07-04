using NTC.ContextStateMachine;
using SouthBasement.Helpers;

namespace SouthBasement.Basement.Enemies.ArmouredRat.AI
{
    public sealed class ArmouredRatAttackState : State<ArmouredRatAI>
    {
        public ArmouredRatAttackState(ArmouredRatAI stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter()
        {
            Initializer.EnemyAnimator.PlayAttack();
            Attack();
        }

        private void Attack()
        {
            if(!Initializer.CanEnterAttackState())
                return;

            Initializer.CurrentlyAttacking = true;
            Initializer.Flipper.Blocked = true;

            Initializer.Flipper.Flip(FacingDirectionsHelper
                .GetFacingDirectionTo(Initializer.transform, Initializer.TargetSelector.Target.transform));

            Initializer.EnemyAttacker.StartAttack(10, () =>
            {
                Initializer.CurrentlyAttacking = false;
                Initializer.Flipper.Blocked = false;
                Initializer.NeedToDefend = true;
                Initializer.NeedToAttack = false;
                
                Attack();
            });
        }
    }
}