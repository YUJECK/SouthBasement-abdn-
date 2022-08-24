using EnemysAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public abstract class State
{
    public enum StateConditions
    {
        DontWork,
        Working,
        Finished
    }
    
    protected bool canInterrupt = false; //Можно ли прервать состояние во время работы
    protected StateConditions stateCondition = StateConditions.DontWork; //Текущее состояние состояния :\
    //Ивенты
    public UnityEvent onEnter = new UnityEvent(); 
    public UnityEvent onExit = new UnityEvent();
    public UnityEvent onUpdate = new UnityEvent();

    public bool CanInterrupt => canInterrupt;
    public StateConditions StateCondition => stateCondition;

    public virtual void Enter(StateMachine stateMachine) => onEnter.Invoke();
    public virtual void Exit(StateMachine stateMachine) => onExit.Invoke();
    public virtual void Update(StateMachine stateMachine) => onUpdate.Invoke();
}