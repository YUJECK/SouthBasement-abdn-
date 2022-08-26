using Creature;
using UnityEngine;

[CreateAssetMenu(fileName = "AngryRatIdleState", menuName = "States/Enemys/AngryRat/AngryRatIdleState")]
public class AngryRatIdleState : State
{
    public override void EnterState(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Working;
        stateMachine.Animator.StopPlayback();
    }
    public override void UpdateState(StateMachine stateMachine)
    {
        stateMachine.ChooseState();
        onUpdate.Invoke();
    }
}
