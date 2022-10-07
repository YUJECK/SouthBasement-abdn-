using CreaturesAI;
using UnityEngine;

[CreateAssetMenu()]
public class TestIdleState : State
{
    //this is idle state, so we dont need to do anything here(only choose state on update)

    public override void EnterState(StateMachine stateMachine) { }
    public override void ExitState(StateMachine stateMachine) { }

    public override void UpdateState(StateMachine stateMachine) => stateMachine.StateChoosing();
}