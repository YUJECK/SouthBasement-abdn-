using UnityEngine;

namespace CreaturesAI
{
    abstract public class State : ScriptableObject
    {
        [Header("State settings")]
        [SerializeField] private string stateName = "Some state";
        public string StateName => stateName;
            
        abstract public void EnterState(StateMachine stateMachine);
        abstract public void UpdateState(StateMachine stateMachine);
        abstract public void ExitState(StateMachine stateMachine);
    }
}