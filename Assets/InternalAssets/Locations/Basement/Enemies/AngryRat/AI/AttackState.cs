using NTC.ContextStateMachine;

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
            Initializer.CurrentAttacking = true;
            
            Initializer.EnemyAttacker.StartAttack(7, () =>
            {
                Initializer.CurrentAttacking = false;
                Attack();
            });
        }
    }
}