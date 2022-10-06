using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TestCombatState : State
{
    public override void EnterState(StateMachine stateMachine)
    {
        stateMachine.Combat.StartAttackingConstantly();
    }

    public override void ExitState(StateMachine stateMachine)
    {
        stateMachine.ChooseState();
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        stateMachine.Combat.StopAttackingConstantly();
    }
}
