using UnityEngine;

namespace NTC.GlobalStateMachine
{
    public sealed class GlobalStateMachineEntry : MonoBehaviour
    {
        [SerializeField] private GameStates stateOnStart = GameStates.Idle;
        
        private void Start()
        {
            GlobalStateMachine.Push(stateOnStart);
            
            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            GlobalStateMachine.Reset();
        }
    }
}