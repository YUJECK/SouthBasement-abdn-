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
        private AngryRatIdleState idleState = new AngryRatIdleState();
        private AngryRatMovingState movingState = new AngryRatMovingState();
        private AngryRatAttackState attackState = new AngryRatAttackState();
        private AngryRatHealingState heelingState = new AngryRatHealingState();
        private TriggerChecker attackTrigger;

        private void Start()
        {
            moving = GetComponent<Move>();
            dynamicPathFinding = GetComponent<DynamicPathFinding>();
            dynamicPathFinding.SetNewTarget(FindObjectOfType<EnemyTarget>());

            dynamicPathFinding.whenANewPathIsFound.AddListener(moving.SetPath);
            ChangeState(movingState);
        }
        private void Update()
        {
            ChooseState();
            if(CurrentState != null) CurrentState.Update(this);
        }
        public override void ChooseState()
        {
            State nextState = idleState;
            if (!Move.IsStopped) nextState = movingState;
            if (Combat.IsOnTrigger) nextState = attackState;
            if (Health.CurrentHealth < 10) nextState = heelingState; 

            if(CurrentState != nextState) ChangeState(nextState);
        }
    }
}