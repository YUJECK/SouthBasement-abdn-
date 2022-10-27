using CreaturesAI;
using UnityEngine;
using System.Collections;

[CreateAssetMenu()]
public class TestCombatState : State
{
    [SerializeField] private GameObject projectile;

    public override void EnterState(StateMachine stateMachine) 
    {
        stateMachine.StartCoroutine(testPattern(stateMachine));
    }
    public override void ExitState(StateMachine stateMachine) 
    {
        stateMachine.StopCoroutine(testPattern(stateMachine)); 
        stateMachine.StateChoosing();
    }
    public override void UpdateState(StateMachine stateMachine) { }

    public IEnumerator testPattern(StateMachine stateMachine)
    {
        for(int i = 0; i < 10; i++)
        {
            stateMachine.Shooting.Shoot(projectile, 10f, 0f, 20f*i, ForceMode2D.Impulse);   
            yield return new WaitForSeconds(0.01f);
        }

        ExitState(stateMachine);
    }
}