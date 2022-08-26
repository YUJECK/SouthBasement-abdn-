using Creature;
using UnityEngine;

[CreateAssetMenu(fileName = "AngryRatMovingState", menuName = "States/Enemys/AngryRat/AngryRatMovingState")]
public sealed class AngryRatMovingState : State
{
    [Header("Параметры передвижения")]
    [SerializeField] private float moveSpeed = 3;

    public override void EnterState(StateMachine stateMachine)
    {
        stateMachine.DynamicPathFinding.StartDynamicPathfinding();
        stateMachine.Animator.Play("Walking");
        onEnter.Invoke();
    }
    public override void UpdateState(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Working;
        stateMachine.Move.Moving();
        stateMachine.ChooseState();
        onUpdate.Invoke();
    }
    public override void ExitState(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Finished;
        stateMachine.DynamicPathFinding.StopDynamicPathfinding();
    }
}
