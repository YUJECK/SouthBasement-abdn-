using Creature;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AngryRatStunnedState", menuName = "States/Enemys/AngryRat/AngryRatIdleState")]
public sealed class AngryRatStunnedState : State
{
    [SerializeField] private const string stunnedAnimationName = "StunnedAnimation";
    public override void EnterState(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Working;
        stateMachine.PlayAnimation(stunnedAnimationName, 1f);
    }
}
