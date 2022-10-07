using CreaturesAI;
using UnityEngine;

[CreateAssetMenu()]
public class TestCombatState : State
{
    public override void EnterState(StateMachine stateMachine) => stateMachine.Combat.StartAttackingConstantly();
    public override void ExitState(StateMachine stateMachine) => stateMachine.Combat.StopAttackingConstantly();
    public override void UpdateState(StateMachine stateMachine) => stateMachine.StateChoosing();
}