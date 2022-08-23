using EnemysAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryRatIdleState : State
{
    public AngryRatIdleState(bool canInterrupt) => this.canInterrupt = canInterrupt;
    public override void Enter(StateMachine stateMachine) 
    {
        stateCondition = StateConditions.Working;
        stateMachine.Animator.Play("OrangeIdle");
    }
}
