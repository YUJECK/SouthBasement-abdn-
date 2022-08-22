using EnemysAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryRatMovingState : State
{
    public override void Enter(StateMachine stateMachine)
    {
        Debug.Log("Start");
        stateMachine.DynamicPathFinding.StartDynamicPathfinding();
        stateMachine.Animator.Play("Walking");
        onEnter.Invoke();
    }
    public override void Update(StateMachine stateMachine)
    {
        stateMachine.Move.Moving();
        onUpdate.Invoke();
    }
    public override void Exit(StateMachine stateMachine)
    {
        stateMachine.DynamicPathFinding.StartDynamicPathfinding();
    }
}
