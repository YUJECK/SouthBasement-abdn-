using Creature;
using UnityEngine;

[CreateAssetMenu(fileName = "AngryRatMovingState", menuName = "States/Enemys/AngryRat/AngryRatMovingState")]
public sealed class AngryRatMovingState : State
{
    public override void EnterState(StateMachine stateMachine)
    {
        stateMachine.DynamicPathFinding.StartDynamicPathfinding();
        stateMachine.Animator.Play("Walking");
        onEnter.Invoke();
    }
    public override void UpdateState(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Working;
        stateMachine.Move.Moving();
        onUpdate.Invoke();
    }
    public override void ExitState(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Finished;
        stateMachine.DynamicPathFinding.StopDynamicPathfinding();
    }
}
