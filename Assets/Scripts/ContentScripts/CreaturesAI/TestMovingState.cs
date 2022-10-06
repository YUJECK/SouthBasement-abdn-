using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TestMovingState : State
{
    public override void EnterState(StateMachine stateMachine)
    {
        stateMachine.DynamicPathfinding.StartPathfinding(stateMachine.TargetSelection.CurrentTarget);
    }

    public override void ExitState(StateMachine stateMachine)
    {
        stateMachine.DynamicPathfinding.StopPathfinding();
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        stateMachine.Moving.Move();
        stateMachine.ChooseState();
    }
}
