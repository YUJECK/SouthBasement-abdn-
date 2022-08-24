using Creature;

public class AngryRatHealingState : State
{
    public AngryRatHealingState(bool canInterrupt, string name) { this.canInterrupt = canInterrupt; stateName = name; }
    public override void Enter(StateMachine stateMachine)
    {
        stateMachine.Animator.Play("OrangeIdle");
        stateMachine.Health.EffectHandler.GetEffect(100, new EffectStats(2, 5), EffectsList.Regeneration);
        stateMachine.Move.SetStop(true);
        stateMachine.Move.BlockStop(true);
    }
    public override void Update(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Working;
        if (stateMachine.Health.CurrentHealth >= 50)
            Exit(stateMachine);
    }
    public override void Exit(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Finished;
        stateMachine.Health.EffectHandler.ResetEffect(EffectsList.Regeneration);
        stateMachine.Move.SetStop(false);
        stateMachine.Move.BlockStop(false);
    }
}
