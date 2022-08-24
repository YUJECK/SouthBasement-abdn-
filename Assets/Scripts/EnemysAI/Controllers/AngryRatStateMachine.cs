using EnemysAI.CombatSkills;
using EnemysAI.Moving;
using EnemysAI.Other;
using UnityEngine;

namespace EnemysAI.Controllers
{
    [RequireComponent(typeof(Sleeping))]
    [RequireComponent(typeof(DynamicPathfinding))]
    [RequireComponent(typeof(Move))]
    [AddComponentMenu("EnemysAI/Controllers/Angry Rat State Machine")]
    public class AngryRatStateMachine : StateMachine
    {
        private AngryRatIdleState idleState = new AngryRatIdleState(true, "AngryRatIdleState");
        private AngryRatMovingState movingState = new AngryRatMovingState(true, "AngryRatMovingState");
        private AngryRatAttackState attackState = new AngryRatAttackState(false, "AngryRatAttackingState");
        private AngryRatHealingState healingState = new AngryRatHealingState(false, "AngryRatHealingState");
        private TriggerChecker attackTrigger;

        public override void ChooseState()
        {
            Debug.Log("Choosing State");
            State nextState = idleState;
            if (DynamicPathFinding.Target != null) { nextState = movingState; }
            if (Combat.IsOnTrigger) nextState = attackState;
            if (Health.CurrentHealth < 10) nextState = healingState;

            if (CurrentState != nextState) ChangeState(nextState);
        }

        private void Start()
        {
            //Инициализация компонентов которые находятся на этом объекте
            moving = GetComponent<Move>();
            dynamicPathFinding = GetComponent<DynamicPathfinding>();
            sleeping = GetComponent<Sleeping>();
            //Добавляем лисэнеров в ивенты
            targetSelection.onSetTarget.AddListener(dynamicPathFinding.SetNewTarget);
            dynamicPathFinding.whenANewPathIsFound.AddListener(moving.SetPath);
            //Ставим idle состояние
            ChangeState(idleState);
        }
        private void Update()
        {
            if (CurrentState.CanInterrupt) ChooseState();
            if (CurrentState != null) CurrentState.Update(this);
        }
    }
}