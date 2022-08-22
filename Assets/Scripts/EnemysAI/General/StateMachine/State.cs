using EnemysAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public abstract class State
{
    [SerializeField] private EnemyState state = EnemyState.Idle;
    [SerializeField] private Animator animator;
    [SerializeField] private string animation = "No animation";
    [SerializeField] protected UnityEvent onEnter = new UnityEvent();
    [SerializeField] protected UnityEvent onExit = new UnityEvent();
    [SerializeField] protected UnityEvent onUpdate = new UnityEvent();

    public EnemyState EnemyState => state;
    public Animator Animator => animator;
    public string Animation => animation;

    public virtual void Enter(StateMachine stateMachine) => onEnter.Invoke();
    public virtual void Exit(StateMachine stateMachine) => onExit.Invoke();
    public virtual void Update(StateMachine stateMachine) => onUpdate.Invoke();
}