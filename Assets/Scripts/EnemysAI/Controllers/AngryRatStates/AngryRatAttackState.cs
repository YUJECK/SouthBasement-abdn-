using EnemysAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryRatAttackState : State
{
    public AngryRatAttackState(bool canInterrupt) => this.canInterrupt = canInterrupt;
    public override void Enter(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Working;
        stateMachine.Animator.Play("Апельсины атакуют");
        stateMachine.Move.SetStop(true);
        stateMachine.Move.BlockStop(true);

        stateMachine.Combat.Attack();
        onEnter.Invoke();
        Utility.InvokeMethod(Exit, stateMachine, stateMachine.Animator.GetCurrentAnimatorClipInfo(0).Length);
    }
    public override void Exit(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Finished;
        stateMachine.Move.BlockStop(false);
        stateMachine.Move.SetStop(false);
        onExit.Invoke();
    }
}
