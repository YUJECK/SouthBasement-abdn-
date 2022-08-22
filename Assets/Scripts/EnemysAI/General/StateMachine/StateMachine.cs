using EnemysAI.CombatSkills;
using EnemysAI.Moving;
using EnemysAI.Other;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EnemysAI
{
    public enum EnemyState
    {
        Idle,
        Sleeping,
        WakeUp,
        Walking,
        Attacking,
        Heeling,
        Stunned
    }

    public abstract class StateMachine : MonoBehaviour
    {
        protected State currentState;
        protected List<Vector2> path;
        [Header("Компоненты")]
        [SerializeField] protected Animator animator; 
        [SerializeField] protected Sleeping sleeping;
        [SerializeField] protected Health health;
        [SerializeField] protected Move moving;
        [SerializeField] protected DynamicPathFinding dynamicPathFinding;
        [SerializeField] protected TargetSelection targetSelection;
        [SerializeField] protected Combat combat;

        public Animator Animator => animator;
        public Sleeping Sleeping => sleeping;
        public Health Health => health;
        public Move Move => moving;
        public DynamicPathFinding DynamicPathFinding => dynamicPathFinding;
        public TargetSelection TargetSelection => targetSelection;
        public Combat Combat => combat;

        public void ChangeState(State newState)
        {
            if (currentState != null) currentState.Exit(this);
            currentState = newState;
            currentState.Enter(this);
            if (currentState.Animation != "No animation" && currentState.Animation != "") currentState.Animator.Play(currentState.Animation);
        }
    }
}
