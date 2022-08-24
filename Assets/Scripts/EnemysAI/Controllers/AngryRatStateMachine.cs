using EnemysAI.CombatSkills;
using EnemysAI.Moving;
using EnemysAI.Other;
using UnityEngine;
using UnityEditor.Events;
using UnityEngine.Events;

namespace EnemysAI.Controllers
{
    [RequireComponent(typeof(Sleeping))]
    [RequireComponent(typeof(DynamicPathFinding))]
    [RequireComponent(typeof(Move))]
    public class AngryRatStateMachine : StateMachine
    {
        private AngryRatIdleState idleState = new AngryRatIdleState(true);
        private AngryRatMovingState movingState = new AngryRatMovingState(true);
        private AngryRatAttackState attackState = new AngryRatAttackState(false);
        private AngryRatHealingState heelingState = new AngryRatHealingState(false);
        private TriggerChecker attackTrigger;

        public override void ChooseState()
        {
            State nextState = idleState;
            if (DynamicPathFinding.Target != null) { nextState = movingState; }
            if (Combat.IsOnTrigger) nextState = attackState;
            if (Health.CurrentHealth < 10) nextState = heelingState; 

            if(CurrentState != nextState) ChangeState(nextState);
        }
        
        private void Start()
        {
            //Инициализация компонентов которые находятся на этом объекте
            moving = GetComponent<Move>();
            dynamicPathFinding = GetComponent<DynamicPathFinding>();
            sleeping = GetComponent<Sleeping>();
            //Добавляем лисэнеров в ивенты
            targetSelection.onSetTarget.AddListener(dynamicPathFinding.SetNewTarget);
            dynamicPathFinding.whenANewPathIsFound.AddListener(moving.SetPath);
            //Ставим idle состояние
            ChangeState(idleState); 
        }
        private void Update()
        {
            if(CurrentState.CanInterrupt) ChooseState();
            if (CurrentState != null) CurrentState.Update(this);
        }
    }
}