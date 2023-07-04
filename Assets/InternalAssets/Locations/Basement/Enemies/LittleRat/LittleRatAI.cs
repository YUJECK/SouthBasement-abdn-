using SouthBasement.AI;
using SouthBasement.AI.MovePoints;

namespace SouthBasement.Basement.Enemies.LittleRat
{
    public sealed class LittleRatAI : AngryRatStateMachine, IMovePointsRequire
    {
        public MovePointsHandler MovePointsHandler;

        public bool CanRunAway;

        public void Initialize(MovePointsHandler movePoints)
            => MovePointsHandler = movePoints;

        protected override void CreateStates()
        {
            StateMachine.AddStates(new IdleState(this), new WalkState(this), new AngryRatAttackState(this), new AFKState(this), new LittleRunAwayState(this));

            StateMachine.AddAnyTransition<AFKState>(CanEnterAFK);
            StateMachine.AddAnyTransition<IdleState>(CanEnterIdleState);
            StateMachine.AddTransition<IdleState, WalkState>(CanEnterWalkState);
            StateMachine.AddTransition<IdleState, AngryRatAttackState>(CanEnterAttackState);
            StateMachine.AddTransition<WalkState, AngryRatAttackState>(CanEnterAttackState);
            StateMachine.AddTransition<AngryRatAttackState, LittleRunAwayState>(CanEnterRunAwayState);

            StateMachine.TransitionsEnabled = true;
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