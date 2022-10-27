using CreaturesAI.Pathfinding;
using UnityEngine;
using TMPro;

namespace CreaturesAI
{
    abstract public class StateMachine : MonoBehaviour
    {
        //variables
        //state info
        [Header("Some info")]
        private State currentState;
        [SerializeField] private string currentStateName;

        //some components
        [Header("Components")]
        [SerializeField] private TMP_Text text;
        [SerializeField] private Shooting shooting;
        [SerializeField] private Moving moving;
        [SerializeField] private TargetSelection targetSelection;
        [SerializeField] private DynamicPathfinding dymamicPathfinding;
        [SerializeField] private Health health;
        [SerializeField] private Combat combat;

        //getters
        protected State CurrentState => currentState;
        public Moving Moving => moving;
        public Shooting Shooting => shooting;
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
                currentState = Instantiate(newState);
                currentStateName = currentState.StateName;
                currentState.EnterState(this);
                text.SetText(currentStateName);
            }
        }

        //abstract methods
        abstract public void StateChoosing();
        abstract protected void UpdateStates();

        //unity methods
        private void Start() => StateChoosing();
        private void Update() => UpdateStates(); 
    }
}