using EnemysAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryRatMovingState : State
{
    public AngryRatMovingState(bool canInterrupt) => this.canInterrupt = canInterrupt;

    public override void Enter(StateMachine stateMachine)
    {
        stateMachine.DynamicPathFinding.StartDynamicPathfinding();
        stateMachine.Animator.Play("Walking");
        onEnter.Invoke();
    }
    public override void Update(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Working;
        stateMachine.Move.Moving();
        onUpdate.Invoke();
    }
    public override void Exit(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Finished;
        stateMachine.DynamicPathFinding.StartDynamicPathfinding();
    }
}
