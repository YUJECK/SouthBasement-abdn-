using Creature;

public class AngryRatIdleState : State
{
    public AngryRatIdleState(bool canInterrupt, string name) { this.canInterrupt = canInterrupt; stateName = name; }
    public override void Enter(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Working;
        stateMachine.Animator.StopPlayback();
    }
}
