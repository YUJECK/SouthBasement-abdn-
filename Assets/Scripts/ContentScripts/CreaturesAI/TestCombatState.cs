using CreaturesAI;
using UnityEngine;

[CreateAssetMenu()]
public class TestCombatState : State
{
    [SerializeField] private GameObject projectile;
    public override void EnterState(StateMachine stateMachine) 
    {
        stateMachine.Shooting.Shoot(projectile, 10f, 0f, 40f, ForceMode2D.Impulse);
    }
    public override void ExitState(StateMachine stateMachine) { }
    public override void UpdateState(StateMachine stateMachine) => stateMachine.StateChoosing();
}