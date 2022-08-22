using EnemysAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryRatAttackState : State
{
    public override void Enter(StateMachine stateMachine)
    {
        stateMachine.Animator.Play("Апельсины атакуют");
        stateMachine.Move.SetStop(true);
        stateMachine.Move.BlockStop(true);

        stateMachine.Combat.Attack();
        onEnter.Invoke();
    }
    public override void Update(StateMachine stateMachine) => onUpdate.Invoke();
    public override void Exit(StateMachine stateMachine) 
    {
        stateMachine.Move.BlockStop(false);
        stateMachine.Move.SetStop(false);
        onExit.Invoke();
    }
}
