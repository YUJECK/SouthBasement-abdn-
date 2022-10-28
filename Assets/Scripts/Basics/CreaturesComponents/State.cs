using UnityEngine;

namespace CreaturesAI
{
    abstract public class State : ScriptableObject
    {
        #region variables
        [Header("State settings")]
        [SerializeField] private string stateName = "Some state";
        [SerializeField] private float stateTransitionDelay = 0f;
        #endregion
        
        #region getters
        public string StateName => stateName;
        public float StateTransitionDelay => stateTransitionDelay;
        #endregion
        
        #region abstract methods
        abstract public void EnterState(StateMachine stateMachine);
        abstract public void UpdateState(StateMachine stateMachine);
        abstract public void ExitState(StateMachine stateMachine);
        #endregion
    }
}