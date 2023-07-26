using CreaturesAI;
using UnityEngine;

[CreateAssetMenu()]
public class TestCombatState : State
{
    [SerializeField] private ShootingPattern simpleShootingPattern;
    [SerializeField] private ShootingPattern roundPattern;

    private ShootingPattern currentPattern;

    public override void EnterState(StateMachine stateMachine)
    {
        if (Vector2.Distance(stateMachine.transform.position, stateMachine.TargetSelection.CurrentTarget.position) < 1.5f)
            currentPattern = Instantiate(roundPattern);
        else currentPattern = Instantiate(simpleShootingPattern);

        currentPattern.UsePattern(stateMachine.Shooting);
    }

    public override void ExitState(StateMachine stateMachine) { }
    public override void UpdateState(StateMachine stateMachine) 
    {
        if (currentPattern.IsFinished)
            stateMachine.StateChoosing();
    }
}