using EnemysAI.CombatSkills;
using EnemysAI.Moving;
using EnemysAI.Other;
using UnityEngine;
using UnityEditor.Events;
using UnityEngine.Events;

namespace EnemysAI.Controllers
{
    [RequireComponent(typeof(Sleeping))]
    public class AngryRatStateMachine : StateMachine
    {
        private AngryRatIdleState idleState = new AngryRatIdleState(true);
        private AngryRatMovingState movingState = new AngryRatMovingState(true);
        private AngryRatAttackState attackState = new AngryRatAttackState(false);
        private AngryRatHealingState heelingState = new AngryRatHealingState(false);
        private TriggerChecker attackTrigger;

        public override void ChooseState()
        {
            if (CurrentState.stateCondition == State.StateConditions.Finished || CurrentState.CanInterrupt)
            {
                State nextState = idleState;
                if (!Move.IsStopped){ nextState = movingState; }
                if (Combat.IsOnTrigger) nextState = attackState;
                if (Health.CurrentHealth < 10) nextState = heelingState; 

                if(CurrentState != nextState) ChangeState(nextState);
            }
        }
        
        private void Start()
        {
            moving = GetComponent<Move>();
            dynamicPathFinding = GetComponent<DynamicPathFinding>();
            dynamicPathFinding.SetNewTarget(FindObjectOfType<EnemyTarget>());

            dynamicPathFinding.whenANewPathIsFound.AddListener(moving.SetPath);
            ChangeState(idleState);
        }
        private void Update()
        {
            if(CurrentState.CanInterrupt) ChooseState();
            if(CurrentState != null) CurrentState.Update(this);
        }
    }
}