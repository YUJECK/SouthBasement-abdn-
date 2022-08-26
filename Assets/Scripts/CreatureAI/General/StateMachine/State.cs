using Creature;
using UnityEngine.Events;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public enum StateConditions
    {
        DontWork,
        Working,
        Finished
    }

    [SerializeField] protected string stateName = "Some state"; //Имя состояния. Не обязательная вещь, просто иногда помогает в дебаге
    [SerializeField] protected bool canInterrupt = false; //Можно ли прервать состояние во время работы
    [SerializeField] protected bool canRepeated = false; //Может повторяться
    [SerializeField] protected bool isDynamicState; //Динамическое ли состояние
    protected StateConditions stateCondition = StateConditions.DontWork; //Текущее состояние состояния :\
    //Ивенты
    public UnityEvent onEnter = new UnityEvent();
    public UnityEvent onFinish = new UnityEvent();
    public UnityEvent onExit = new UnityEvent();
    public UnityEvent onUpdate = new UnityEvent();

    //Геттеры
    public bool CanInterrupt => canInterrupt;
    public bool CanRepeated => canRepeated;
    public bool IsDynamicState => isDynamicState;
    public StateConditions StateCondition => stateCondition;
    public string StateName => stateName;

    //Методы в которых описывается поведение состояния
    public virtual void EnterState(StateMachine stateMachine) => onEnter.Invoke();
    public virtual void FinishState(StateMachine stateMachine) { stateCondition = StateConditions.Finished; onFinish.Invoke(); } 
    public virtual void ExitState(StateMachine stateMachine) => onExit.Invoke();
    public virtual void UpdateState(StateMachine stateMachine) => onUpdate.Invoke();
}