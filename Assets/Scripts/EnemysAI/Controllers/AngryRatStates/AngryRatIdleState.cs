using EnemysAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryRatIdleState : State
{
    public override void Enter(StateMachine stateMachine) => stateMachine.Animator.Play("OrangeIdle");
}
