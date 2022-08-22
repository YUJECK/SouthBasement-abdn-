using EnemysAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryRatHealingState : State
{
    public override void Enter(StateMachine stateMachine)
    {
        stateMachine.Animator.Play("OrangeIdle");
        stateMachine.Health.EffectHandler.GetEffect(100, new EffectStats(2, 5), EffectsList.Regeneration);
        stateMachine.Move.SetStop(true);
        stateMachine.Move.BlockStop(true);
    }
    public override void Update(StateMachine stateMachine)
    {
        if (stateMachine.Health.CurrentHealth >= 50)
            Exit(stateMachine);
    }
    public override void Exit(StateMachine stateMachine)
    {
        stateMachine.Health.EffectHandler.ResetEffect(EffectsList.Regeneration); 
        stateMachine.Move.SetStop(false);
        stateMachine.Move.BlockStop(false);
    }
}
