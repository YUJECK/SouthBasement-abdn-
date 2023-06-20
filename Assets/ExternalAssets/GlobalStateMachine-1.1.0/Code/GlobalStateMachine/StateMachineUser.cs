using UnityEngine;

namespace NTC.GlobalStateMachine
{
    public abstract class StateMachineUser : MonoBehaviour
    {
        private void Awake()
        {
            BindCallbacks();
            OnAwake();
        }

        private void OnDestroy()
        {
            this.RemoveSubscriber();
            
            OnDestroyOverridable();
        }

        private void BindCallbacks()
        {
            this.On<IdleState>(OnGameIdle);
            this.On<NPCState>(OnNPC);
            this.On<FightState>(OnFight);
            this.On<DiedState>(OnDied);
            this.On<PausedState>(OnGamePause);
        }

        protected virtual void OnAwake() { }
        protected virtual void OnDestroyOverridable() { }
        
        protected virtual void OnGameIdle() { }
        protected virtual void OnNPC() { }
        protected virtual void OnFight() { }
        protected virtual void OnDied() { }
        protected virtual void OnGamePause() { }
    }
}