using EnemysAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public abstract class State
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animation = "No animation";
    public UnityEvent onEnter = new UnityEvent();
    public UnityEvent onExit = new UnityEvent();
    public UnityEvent onUpdate = new UnityEvent();

    public Animator Animator => animator;
    public string Animation => animation;

    public virtual void Enter(StateMachine stateMachine) => onEnter.Invoke();
    public virtual void Exit(StateMachine stateMachine) => onExit.Invoke();
    public virtual void Update(StateMachine stateMachine) => onUpdate.Invoke();
}