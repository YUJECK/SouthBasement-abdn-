using SouthBasement.AI;

namespace SouthBasement.Basement.Enemies.LittleRat
{
    public sealed class LittleRatAI : AngryRatStateMachine, IMovePointsRequire
    {
        public MovePoint[] MovePoints;

        public bool CanRunAway;

        protected override void CreateStates()
        {
            StateMachine.AddStates(new IdleState(this), new WalkState(this), new AngryRatAttackState(this), new AFKState(this), new LittleRunAwayState(this));

            StateMachine.AddAnyTransition<AFKState>(CanEnterAFK);
            StateMachine.AddAnyTransition<AngryRatAttackState>(CanEnterAttackState);
            StateMachine.AddAnyTransition<WalkState>(CanEnterWalkState);
            StateMachine.AddAnyTransition<IdleState>(CanEnterIdleState);
            StateMachine.AddTransition<AngryRatAttackState, LittleRunAwayState>(CanEnterRunAwayState);

            StateMachine.TransitionsEnabled = true;
        }

        public override bool CanEnterAttackState()
            => base.CanEnterAttackState() && !CanRunAway;
        
        public override bool CanEnterWalkState()
            => base.CanEnterWalkState() && !CanRunAway;
        
        public bool CanEnterRunAwayState()
            => Enabled && CanRunAway;

        public void Initialize(MovePoint[] movePoints)
            => MovePoints = movePoints;
    }
}