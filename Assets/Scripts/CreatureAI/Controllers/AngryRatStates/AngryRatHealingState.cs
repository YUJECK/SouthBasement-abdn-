using Creature;
using UnityEngine;


[CreateAssetMenu(fileName = "AngryRatHealingState", menuName = "States/Enemys/AngryRat/AngryRatHealingState")]
public class AngryRatHealingState : State
{
    public AngryRatHealingState(bool canInterrupt, bool canRepeated, bool isDynamic, string name) 
    {
        this.mustBeFinished = canInterrupt;
        this.canRepeated = canRepeated; 
        isDynamicState = isDynamic; 
        stateName = name; 
    }
    public override void EnterState(StateMachine stateMachine)
    {
        stateMachine.Animator.Play("OrangeIdle");
        stateMachine.Health.EffectHandler.AddEffect(100, new EffectStats(2, 5), EffectsList.Regeneration);
        onEnter.Invoke();   
    }
    public override void UpdateState(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Working;
        onUpdate.Invoke();
        if (stateMachine.Health.CurrentHealth >= 50)
            FinishState(stateMachine);
    }
    public override void FinishState(StateMachine stateMachine)
    {
        stateCondition = StateConditions.Finished;
        stateMachine.Health.EffectHandler.RemoveEffect(EffectsList.Regeneration);
        onFinish.Invoke();
    }
}
