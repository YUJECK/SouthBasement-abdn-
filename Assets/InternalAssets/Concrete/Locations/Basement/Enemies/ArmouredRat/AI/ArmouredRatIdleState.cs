using NTC.ContextStateMachine;

namespace SouthBasement.Basement.Enemies.ArmouredRat.AI
{
    public sealed class ArmouredRatIdleState : State<ArmouredRatAI>
    {
        public ArmouredRatIdleState(ArmouredRatAI stateInitializer) : base(stateInitializer)
        {
        }

        protected override void OnEnter()
        {
            Initializer.EnemyAnimator.PlayIdle();
        }
    }
}