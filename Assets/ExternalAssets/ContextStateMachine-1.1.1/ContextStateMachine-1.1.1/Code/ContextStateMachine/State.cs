using System;

namespace NTC.ContextStateMachine
{
    public abstract class State<TInitializer>
    {
        public TInitializer Initializer { get; }
        public bool IsActive { get; set; }

        public event Action OnEntered;
        public event Action OnExited;
        
        protected State(TInitializer stateInitializer)
        {
            Initializer = stateInitializer;
        }

        public void Enter()
        {
            OnEnter();
            OnEntered?.Invoke(); 
        }
        public void Run() => OnRun();
        public void Exit()
        {
            OnExit();
            OnExited?.Invoke(); 
        }

        protected virtual void OnEnter() { }
        protected virtual void OnRun() { }
        protected virtual void OnExit() { }
    }
}