using CreaturesAI;
using UnityEngine;

[CreateAssetMenu()]
public class TestMovingState : State
{
    [Space(10)]
    [SerializeField] private float moveSpeed;

    public override void EnterState(StateMachine stateMachine)
    {
        stateMachine.DynamicPathfinding.StartPathfinding(stateMachine.TargetSelection.CurrentTarget);
    }

    public override void ExitState(StateMachine stateMachine)
    {
        stateMachine.DynamicPathfinding.StopPathfinding();
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        stateMachine.Moving.Move(moveSpeed);
        stateMachine.StateChoosing();
    }
}
