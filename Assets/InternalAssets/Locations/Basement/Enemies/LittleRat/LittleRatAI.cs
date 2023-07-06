using SouthBasement.AI;
using SouthBasement.AI.MovePoints;

namespace SouthBasement.Basement.Enemies.LittleRat
{
    public sealed class LittleRatAI : AngryRatStateMachine, IMovePointsRequire
    {
        public int Damage = 4;
        public MovePointsHandler MovePointsHandler;

        public bool CanRunAway;

        public void Initialize(MovePointsHandler movePoints)
            => MovePointsHandler = movePoints;

        protected override void CreateStates()
        {
            StateMachine.AddStates(new AngryRatIdleState(this), new AngryRatWalkState(this), new AngryRatAttackState(this), new AngryRatAFKState(this), new LittleRunAwayState(this));

            StateMachine.AddAnyTransition<AngryRatAFKState>(CanEnterAFK);
            StateMachine.AddAnyTransition<AngryRatIdleState>(CanEnterIdleState);
            StateMachine.AddTransition<AngryRatIdleState, AngryRatWalkState>(CanEnterWalkState);
            StateMachine.AddTransition<AngryRatIdleState, AngryRatAttackState>(CanEnterAttackState);
            StateMachine.AddTransition<AngryRatWalkState, AngryRatAttackState>(CanEnterAttackState);
            StateMachine.AddTransition<AngryRatAttackState, LittleRunAwayState>(CanEnterRunAwayState);
        }

        public override bool CanEnterAttackState()
            => base.CanEnterAttackState() && !CanRunAway;

        public override bool CanEnterWalkState()
            => base.CanEnterWalkState() && !CanRunAway;

        public override bool CanEnterIdleState()
            => base.CanEnterIdleState() && !CanRunAway;

        public bool CanEnterRunAwayState()
            => Enabled && CanRunAway;
    }
}