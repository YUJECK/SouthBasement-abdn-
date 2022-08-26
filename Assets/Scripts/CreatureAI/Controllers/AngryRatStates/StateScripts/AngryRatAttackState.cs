using Creature;
using UnityEngine; 

[CreateAssetMenu(fileName = "AngryRatAttackState", menuName = "States/Enemys/AngryRat/AngryRatAttackState")]
public sealed class AngryRatAttackState : State
{
    public override void EnterState(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Working;
        stateMachine.PlayAnimation("Апельсины атакуют", 1f);

        stateMachine.Combat.Attack();
        onEnter.Invoke();
        Utility.InvokeMethod(FinishState, stateMachine, stateMachine.Animator.GetCurrentAnimatorStateInfo(0).length);
    }
    public override void FinishState(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Finished;
        onFinish.Invoke();
    }
}
