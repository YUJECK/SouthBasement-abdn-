using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class State : ScriptableObject
{
    [SerializeField] private string stateName = "Some state";
    public string StateName => stateName;

    abstract public void EnterState(StateMachine stateMachine);
    abstract public void UpdateState(StateMachine stateMachine);
    abstract public void ExitState(StateMachine stateMachine);
}