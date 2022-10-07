using CreaturesAI;
using UnityEngine;

public class TestStateMachine : StateMachine
{
    [Header("States")]
    [SerializeField] private State idleState;
    [SerializeField] private State movingState;
    [SerializeField] private State attackingState;

    public override void StateChoosing()
    {
        State newState = idleState;

        if (TargetSelection.TargetsCount > 0)
            newState = movingState;
        if (Combat.TriggerChecker.IsOnTrigger)
            newState = attackingState;

        ChangeState(newState);
    }

    protected override void UpdateStates()
    {
        if (CurrentState != null)
            CurrentState.UpdateState(this);
    }
}
