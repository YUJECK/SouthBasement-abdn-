using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TestIdleState : State
{
    //this is idle state, so we dont need to do anything here(only choose state on exit)

    public override void EnterState(StateMachine stateMachine)
    {
    }

    public override void ExitState(StateMachine stateMachine)
    {
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        stateMachine.ChooseState();
    }
}
