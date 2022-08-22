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
        private AngryRatMovingState movingState = new AngryRatMovingState();
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
           if(currentState != null) currentState.Update(this);
        }
    }
}