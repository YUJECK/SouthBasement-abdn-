using CreaturesAI.Moving;
using CreaturesAI.Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class StateMachine : MonoBehaviour
{
    //variables
    //state info
    [Header("Some info")]
    private State currentState;
    [SerializeField] private string currentStateName;

    //some components
    [Header("Components")]
    [SerializeField] private Moving moving;
    [SerializeField] private TargetSelection targetSelection;
    [SerializeField] private DynamicPathfinding dymamicPathfinding;
    [SerializeField] private Health health;
    [SerializeField] private Combat combat;

    //getters
    protected State CurrentState => currentState;
    public Moving Moving => moving;
    public TargetSelection TargetSelection => targetSelection;
    public DynamicPathfinding DynamicPathfinding => dymamicPathfinding;
    public Health Health => health;
    public Combat Combat => combat;

    //constant methods
    protected void ChangeState(State newState)
    {
        if(newState != currentState)
        {
            if(currentState != null) currentState.ExitState(this);
            currentState = newState;
            currentStateName = currentState.StateName;
            currentState.EnterState(this);
        }
    }

    //abstract methods
    abstract public void ChooseState();
    abstract protected void UpdateStates();

    //unity methods
    private void Start() => ChooseState();
    private void Update() => UpdateStates(); 
}