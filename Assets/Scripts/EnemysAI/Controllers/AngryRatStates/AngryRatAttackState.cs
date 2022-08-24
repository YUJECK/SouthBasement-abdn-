using EnemysAI;

public class AngryRatAttackState : State
{
    public AngryRatAttackState(bool canInterrupt, string name) { this.canInterrupt = canInterrupt; stateName = name; }
    public override void Enter(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Working;
        stateMachine.Animator.Play("Апельсины атакуют");
        stateMachine.Move.SetStop(true);
        stateMachine.Move.BlockStop(true);

        stateMachine.Combat.Attack();
        Utility.InvokeMethod(Exit, stateMachine, 1f);
        onEnter.Invoke();
    }
    public override void Exit(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Finished;
        stateMachine.Move.BlockStop(false);
        stateMachine.Move.SetStop(false);
        onExit.Invoke();
    }
}
